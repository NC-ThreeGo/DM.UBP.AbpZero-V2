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
                    Status = a.a.Status,
                    TypeTable = a.b.TypeTable,
                    Job_Id = a.a.Job_Id,
                    CronStr = c.CronStr
                });
            
            foreach (var scheduler in schedulers)
            {
                var jobName = "Scheduler_" + scheduler.Id;
                var groupName = "BackgroundJobManager";

                if (scheduler.Status)
                {
                    if (scheduler.TypeTable == "BGJM_JOB_RPTEMAIL")
                    {
                        var jobEmail = await _Job_RPTEmailManager.GetJob_RPTEmailByIdAsync(scheduler.Job_Id);
                        JobDataMap jobDM = new JobDataMap();
                        jobDM.Add("rptId", jobEmail.Template_Id);
                        jobDM.Add("emails", jobEmail.Emails);
                        jobDM.Add("paramters", jobEmail.Parameters);
                        jobDM.Add("job_RPTEmailName", jobEmail.Job_RPTEmailName);

                        _jobManager.RunJob<RPTEmailJob>(
                            job =>
                            {
                                job.WithIdentity(jobName, groupName).SetJobData(jobDM);
                            },
                            trigger =>
                            {
                                trigger.StartNow()
                                .WithIdentity(jobName, groupName)
                                .WithCronSchedule(scheduler.CronStr);
                            });
                    }
                    if (scheduler.TypeTable == "BGJM_JOB_SQL")
                    {
                        var jobSql = await _Job_SqlManager.GetJob_SqlByIdAsync(scheduler.Job_Id);
                        JobDataMap jobDM = new JobDataMap();
                        jobDM.Add("connkeyName", jobSql.ConnkeyName);
                        jobDM.Add("commandType", jobSql.CommandType);
                        jobDM.Add("commandText", jobSql.CommandText);
                        jobDM.Add("paramters", jobSql.Paramters);

                        _jobManager.RunJob<SQLJob>(
                            job =>
                            {
                                job.WithIdentity(jobName, groupName).SetJobData(jobDM);
                            },
                            trigger =>
                            {
                                trigger.StartNow()
                                .WithIdentity(jobName, groupName)
                                .WithCronSchedule(scheduler.CronStr);
                            });
                    }
                }
                else
                {
                    _jobManager.DeleteJob(jobName, groupName);
                }

            }
        }
    }
}
