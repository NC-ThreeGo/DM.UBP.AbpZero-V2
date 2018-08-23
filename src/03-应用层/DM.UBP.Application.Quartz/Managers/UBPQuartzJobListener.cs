using Abp.Quartz.Quartz;
using Castle.Core.Logging;
using DM.UBP.Domain.Service.BackgroundJobManager.Loggers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Quartz.Managers
{
    public class UBPQuartzJobListener : IJobListener
    {
        public string Name { get; } = "UBPJobListener";

        public ILogger Logger { get; set; }

        private readonly ILoggerManager _LoggerManager;

        public UBPQuartzJobListener(ILoggerManager LoggerManager)
        {
            Logger = NullLogger.Instance;
            _LoggerManager = LoggerManager;
        }


        public virtual void JobExecutionVetoed(IJobExecutionContext context)
        {
            Logger.Info($"Job {context.JobDetail.JobType.Name} executing operation vetoed...");
        }

        public virtual void JobToBeExecuted(IJobExecutionContext context)
        {
            //轮询调度任务不写日志
            if (context.JobDetail.JobType.Name == "Job_Scheduler")
                return;

            var jobName = context.JobDetail.JobDataMap["jobName"];
            if (jobName == null)
                return;
            System.Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}：{jobName.ToString()} 开始调度！");

            var jobType = context.JobDetail.JobDataMap["jobType"];
            if (jobType == null)
                return;

            var job_id = context.JobDetail.JobDataMap["jobId"];
            if (job_id == null)
                return;

            DM.UBP.Domain.Entity.BackgroundJobManager.Logger logger = new Domain.Entity.BackgroundJobManager.Logger();
            logger.JobType = jobType.ToString();
            logger.JobName = jobName.ToString();
            logger.Job_Id = Convert.ToInt32(job_id);
            logger.ExecStartTime = DateTime.Now;

            //默认给一个创建用户
            logger.CreatorUserId = 1;
            var loggerId = _LoggerManager.CreateLoggerToGetIdAsync(logger);

            context.Put("loggerId", loggerId.Result);
        }

        public virtual void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            //轮询调度任务不写日志
            if (context.JobDetail.JobType.Name == "Job_Scheduler")
                return;

            var jobName = context.JobDetail.JobDataMap["jobName"];
            if (jobName == null)
                return;

            if (jobException == null)
                System.Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}：{jobName.ToString()} 结束调度！");
            else
                System.Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}：{jobName.ToString()} 调度任务异常！异常信息：{jobException}");

            var loggerId = context.Get("loggerId");
            if (loggerId == null)
                return;

            var entityLogger = _LoggerManager.GetLoggerByIdAsync(Convert.ToInt32(loggerId)).Result;
            if (entityLogger == null)
                return;

            entityLogger.ExecEndTime = DateTime.Now;
            if (jobException == null)
            {
                entityLogger.IsException = false;
                entityLogger.Note = "调度成功";
            }
            else
            {
                entityLogger.IsException = true;
                entityLogger.Note = $"调度异常：{jobException.InnerException.InnerException.Message}";
            }
            _LoggerManager.UpdateLoggerAsync(entityLogger);

        }
    }
}
