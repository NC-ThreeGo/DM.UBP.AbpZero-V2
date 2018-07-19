using Abp.Quartz.Quartz;
using Castle.Core.Logging;
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

        public UBPQuartzJobListener()
        {
            Logger = NullLogger.Instance;
        }


        public virtual void JobExecutionVetoed(IJobExecutionContext context)
        {
            Logger.Info($"Job {context.JobDetail.JobType.Name} executing operation vetoed...");
        }

        public virtual void JobToBeExecuted(IJobExecutionContext context)
        {
            if (context.JobDetail.JobType.Name == "Job_Scheduler")
                return;

            var schedulerName = context.JobDetail.JobDataMap["schedulerName"];
            if (schedulerName == null)
                return;
            System.Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}：{schedulerName.ToString()} 开始调度！");
        }

        public virtual void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            if (context.JobDetail.JobType.Name == "Job_Scheduler")
                return;

            var schedulerName = context.JobDetail.JobDataMap["schedulerName"];
            if (schedulerName == null)
                return;

            if (jobException == null)
            {
                System.Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}：{schedulerName.ToString()} 结束调度！");
            }
            else
            {
                System.Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}：{schedulerName.ToString()} 调度任务异常！异常信息：{jobException}");
            }
        }
    }
}
