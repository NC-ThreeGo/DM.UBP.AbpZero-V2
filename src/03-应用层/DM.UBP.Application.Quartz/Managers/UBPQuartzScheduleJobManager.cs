using Abp.BackgroundJobs;
using Abp.Quartz.Quartz;
using Abp.Quartz.Quartz.Configuration;
using Quartz;
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

        public void RunJob<TJob>(Action<JobBuilder> configureJob, Action<TriggerBuilder> configureTrigger)
            where TJob : IJob
        {
            var jobToBuild = JobBuilder.Create<TJob>();
            configureJob(jobToBuild);
            var job = jobToBuild.Build();

            var triggerToBuild = TriggerBuilder.Create();
            configureTrigger(triggerToBuild);
            var trigger = triggerToBuild.Build();

            if (_quartzConfiguration.Scheduler.CheckExists(job.Key))
            {
                //_quartzConfiguration.Scheduler.UnscheduleJob
                //var aa = _quartzConfiguration.Scheduler.GetTriggerState(trigger.Key);

                _quartzConfiguration.Scheduler.DeleteJob(job.Key);
            }
            _quartzConfiguration.Scheduler.ScheduleJob(job, trigger);
            //return;
            //ResumeJob(job.Key.Name,job.Key.Group);
        }

        public void DeleteJob(string jobName, string groupName)
        {
            JobKey jk = new JobKey(jobName, groupName);
            if (!_quartzConfiguration.Scheduler.CheckExists(jk))
                return;
            _quartzConfiguration.Scheduler.DeleteJob(jk);
        }

        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        public void PauseJob(string jobName, string groupName)
        {
            JobKey jk = new JobKey(jobName, groupName);
            if (!_quartzConfiguration.Scheduler.CheckExists(jk))
                return;
            _quartzConfiguration.Scheduler.PauseJob(jk);
        }

        /// <summary>
        /// 重新开始
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        private void ResumeJob(string jobName, string groupName)
        {
            JobKey jk = new JobKey(jobName, groupName);
            if (!_quartzConfiguration.Scheduler.CheckExists(jk))
                return;
            _quartzConfiguration.Scheduler.ResumeJob(jk);
        }
    }
}
