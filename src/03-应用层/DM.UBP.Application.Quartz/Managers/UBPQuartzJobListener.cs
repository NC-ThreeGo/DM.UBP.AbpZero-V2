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
            //轮询调度任务不写日志
            if (context.JobDetail.JobType.Name == "Job_Scheduler")
                return;

            var jobName = context.JobDetail.JobDataMap["jobName"];
            if (jobName == null)
                return;
            System.Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}：{jobName.ToString()} 开始调度！");
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
            {
                System.Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}：{jobName.ToString()} 结束调度！");
            }
            else
            {
                System.Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}：{jobName.ToString()} 调度任务异常！异常信息：{jobException}");
            }
        }
    }
}
