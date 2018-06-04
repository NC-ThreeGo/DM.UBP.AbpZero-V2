﻿using Abp.Quartz.Quartz;
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
            Logger.Debug($"Job {context.JobDetail.JobType.Name} executing...");
        }

        public virtual void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            if (jobException == null)
            {
                Logger.Debug($"Job {context.JobDetail.JobType.Name} sucessfully executed.");
            }
            else
            {
                Logger.Error($"Job {context.JobDetail.JobType.Name} failed with exception: {jobException}");
            }
        }
    }
}
