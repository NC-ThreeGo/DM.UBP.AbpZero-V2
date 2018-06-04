using Abp.Quartz.Quartz;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Quartz.Managers
{
    public interface IUBPQuartzScheduleJobManager : IQuartzScheduleJobManager
    {
        void RunJob<TJob>(Action<JobBuilder> configureJob, Action<TriggerBuilder> configureTrigger) where TJob : IJob;

        void DeleteJob(string jobName, String groupName);

        void PauseJob(string jobName, String groupName);
    }
}
