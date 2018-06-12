using Devart.Data.Oracle;
using DM.UBP.Application.Quartz.Jobs;
using DM.UBP.Application.Quartz.Managers;
using DM.UBP.Common.DbHelper;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DM.UBP.Application.Quartz.Servers
{
    public class QuartzServer : IQuartzServer
    {
        private readonly IUBPQuartzScheduleJobManager _jobManager;

        public QuartzServer(IUBPQuartzScheduleJobManager jobManager)
        {
            _jobManager = jobManager;
        }

        public void StartUp()
        {
            _jobManager.ScheduleAsync<Job_Scheduler>(
            job =>
            {
                job.WithIdentity("Job_Scheduler", "BackgroundJobManager")
                    .WithDescription("A job to get schedulers.");
            },
            trigger =>
            {
                //trigger.WithCronSchedule("0/30 * * * * ? *");

                trigger.WithSimpleSchedule(schedule =>
                    {
                        schedule.RepeatForever()
                            .WithIntervalInSeconds(30)//间隔30秒
                            .Build();
                    });
            });
            _jobManager.Start();
        }

        /// <summary>
        /// 获取任务在未来周期内哪些时间会运行
        /// </summary>
        /// <param name="CronExpressionString">Cron表达式</param>
        /// <param name="numTimes">运行次数</param>
        /// <returns>运行时间段</returns>
        public List<string> GetTaskFireTime(string CronExpressionString, int numTimes)
        {
            List<string> list = new List<string>();
            try
            {
                //时间表达式
                ITrigger trigger = TriggerBuilder.Create().WithCronSchedule(CronExpressionString).Build();
                IList<DateTimeOffset> dates = TriggerUtils.ComputeFireTimes(trigger as IOperableTrigger, null, numTimes);

                foreach (DateTimeOffset dtf in dates)
                {
                    list.Add(TimeZoneInfo.ConvertTimeFromUtc(dtf.DateTime, TimeZoneInfo.Local).ToString());
                }
                return list;
            }
            catch (Exception ex)
            {
                return list;
            }
        }
    }
}
