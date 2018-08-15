using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Quartz.Quartz;
using Devart.Data.Oracle;
using DM.UBP.Application.Quartz.Managers;
using DM.UBP.Common.DbHelper;
using DM.UBP.Domain.Service.BackgroundJobManager.Job_RPTEmails;
using DM.UBP.Domain.Service.BackgroundJobManager.Job_Sqls;
using DM.UBP.Domain.Service.BackgroundJobManager.JobGroups;
using DM.UBP.Domain.Service.BackgroundJobManager.Schedulers;
using DM.UBP.Domain.Service.BackgroundJobManager.Triggers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Quartz.Jobs
{
    public class Job_Scheduler : JobBase, ITransientDependency
    {
        private readonly ISchedulerManager _SchedulerManager;
        private readonly IJobGroupManager _JobGroupManager;
        private readonly IJob_RPTEmailManager _Job_RPTEmailManager;
        private readonly IJob_SqlManager _Job_SqlManager;
        private readonly ITriggerManager _TriggerManager;

        private readonly IUBPQuartzScheduleJobManager _jobManager;

        public Job_Scheduler(ISchedulerManager schedulermanager,
            IJobGroupManager jobgroupmanager,
            IJob_RPTEmailManager job_rptemailmanager,
            IJob_SqlManager job_sqlmanager,
            ITriggerManager triggermanager,
            IUBPQuartzScheduleJobManager jobManager)
        {
            _SchedulerManager = schedulermanager;
            _JobGroupManager = jobgroupmanager;
            _Job_RPTEmailManager = job_rptemailmanager;
            _Job_SqlManager = job_sqlmanager;
            _TriggerManager = triggermanager;

            _jobManager = jobManager;
        }


        [UnitOfWork]

        public override async void Execute(IJobExecutionContext context)
        {
            var schedulerEntities = await _SchedulerManager.GetAllSchedulersAsync();
            var jobGroupEntities = await _JobGroupManager.GetAllJobGroupsAsync();
            var triggerEnities = await _TriggerManager.GetAllTriggersAsync();

            var schedulers = schedulerEntities.Join(jobGroupEntities, a => a.JobGroup_Id, b => b.Id, (a, b) => new { a, b })
                .Join(triggerEnities, a => a.a.Trigger_Id, c => c.Id, (a, c) => new
                {
                    Id = a.a.Id,
                    Name = a.a.SchedulerName,
                    Status = a.a.Status,
                    TypeTable = a.b.TypeTable,
                    Job_Id = a.a.Job_Id,
                    CronStr = c.CronStr
                });
            
            foreach (var scheduler in schedulers)
            {
                var quartzJobName = "Scheduler_" + scheduler.Id;
                var quartzGroupName = "BackgroundJobManager";
                if (scheduler.Status)
                {
                    execScheduler(quartzJobName, quartzJobName, scheduler.CronStr, scheduler.TypeTable, scheduler.Job_Id, -1);
                }
                else
                {
                    _jobManager.DeleteJob(quartzJobName, quartzGroupName);
                }

            }
        }

        /// <summary>
        /// 执行一个调度
        /// </summary>
        /// <param name="quartzJobName">调度工作名称</param>
        /// <param name="quartzGroupName">调度组名称</param>
        /// <param name="quartzCronStr">调度时间</param>
        /// <param name="jobType">工作类型</param>
        /// <param name="jobId">工作ID</param>
        /// <param name="repeatCount">执行次数0为无线执行</param>
        public async void execScheduler(string quartzJobName,
            string quartzGroupName,
            string quartzCronStr,
            string jobType,
            long jobId,
            int repeatCount)
        {
            if (jobType == "BGJM_JOB_RPTEMAIL")
            {
                var jobEmail = await _Job_RPTEmailManager.GetJob_RPTEmailByIdAsync(jobId);
                JobDataMap jobDM = new JobDataMap();
                jobDM.Add("jobName", jobEmail.Job_RPTEmailName);
                jobDM.Add("jobType", jobType);
                jobDM.Add("jobId", jobEmail.Id);

                jobDM.Add("rptId", jobEmail.Template_Id);
                jobDM.Add("emails", jobEmail.Emails);
                jobDM.Add("paramters", jobEmail.Parameters);
                jobDM.Add("job_RPTEmailName", jobEmail.Job_RPTEmailName);

                //执行次数
                if (repeatCount == -1)
                {
                    _jobManager.RunJob<RPTEmailJob>(
                    job =>
                    {
                        job.WithIdentity(quartzJobName, quartzGroupName).SetJobData(jobDM);
                    },
                    trigger =>
                    {
                        trigger.StartNow()
                        .WithIdentity(quartzJobName, quartzGroupName)
                        .WithCronSchedule(quartzCronStr);
                    });
                }
                else
                {
                    _jobManager.RunJob<RPTEmailJob>(
                   job =>
                   {
                       job.WithIdentity(quartzJobName, quartzGroupName).SetJobData(jobDM);
                   },
                   trigger =>
                   {
                       trigger.StartNow()
                       .WithIdentity(quartzJobName, quartzGroupName)
                       .WithSimpleSchedule(schedule =>
                       {
                           schedule.WithRepeatCount(repeatCount)
                               .WithIntervalInSeconds(1)//立即执行
                               .Build();
                       });
                   });
                }
            }
            if (jobType == "BGJM_JOB_SQL")
            {
                var jobSql = await _Job_SqlManager.GetJob_SqlByIdAsync(jobId);
                JobDataMap jobDM = new JobDataMap();
                jobDM.Add("jobName", jobSql.Job_SqlName);
                jobDM.Add("jobType", jobType);
                jobDM.Add("jobId", jobSql.Id);

                jobDM.Add("connkeyName", jobSql.ConnkeyName);
                jobDM.Add("commandType", jobSql.CommandType);
                jobDM.Add("commandText", jobSql.CommandText);
                jobDM.Add("paramters", jobSql.Paramters);

                if (repeatCount == -1)
                {
                    _jobManager.RunJob<SQLJob>(
                        job =>
                        {
                            job.WithIdentity(quartzJobName, quartzGroupName).SetJobData(jobDM);
                        },
                        trigger =>
                        {
                            trigger.StartNow()
                            .WithIdentity(quartzJobName, quartzGroupName)
                            .WithCronSchedule(quartzCronStr);
                        });
                }
                else
                {
                    _jobManager.RunJob<SQLJob>(
                       job =>
                       {
                           job.WithIdentity(quartzJobName, quartzGroupName).SetJobData(jobDM);
                       },
                       trigger =>
                       {
                           trigger.StartNow()
                           .WithIdentity(quartzJobName, quartzGroupName)
                           .WithSimpleSchedule(schedule =>
                           {
                               schedule.WithRepeatCount(repeatCount)
                                   .WithIntervalInSeconds(1)//立即执行
                                   .Build();
                           });
                       });
                }
            }
        }
    }
}
