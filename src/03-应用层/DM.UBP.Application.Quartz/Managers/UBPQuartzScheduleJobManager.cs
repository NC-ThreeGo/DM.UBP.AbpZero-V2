using Abp.BackgroundJobs;
using Abp.Quartz.Quartz;
using Abp.Quartz.Quartz.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Quartz.Managers
{
    public class UBPQuartzScheduleJobManager : QuartzScheduleJobManager, IUBPQuartzScheduleJobManager
    {
        private readonly IBackgroundJobConfiguration _backgroundJobConfiguration;
        private readonly IAbpQuartzConfiguration _quartzConfiguration;

        public UBPQuartzScheduleJobManager(
            IAbpQuartzConfiguration quartzConfiguration,
            IBackgroundJobConfiguration backgroundJobConfiguration) : base(quartzConfiguration, backgroundJobConfiguration)
        {
            _quartzConfiguration = quartzConfiguration;
            _backgroundJobConfiguration = backgroundJobConfiguration;
        }

    }
}
