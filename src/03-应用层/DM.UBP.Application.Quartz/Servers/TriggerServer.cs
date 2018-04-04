using DM.UBP.Application.Quartz.Jobs;
using DM.UBP.Application.Quartz.Managers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Quartz.Servers
{
    public class TriggerServer : ITriggerServer
    {
        private readonly IUBPQuartzScheduleJobManager _jobManager;

        public TriggerServer(IUBPQuartzScheduleJobManager jobManager)
        {
            _jobManager = jobManager;
        }

        public void ToDoDayJob()
        {

            _jobManager.ScheduleAsync<DayJob>(
            job =>
            {
                job.WithIdentity("MyLogJobIdentity", "MyGroup")
                    .WithDescription("A job to simply write logs.");
            },
            trigger =>
            {
                trigger.StartNow()
                    .WithSimpleSchedule(schedule =>
                    {
                        schedule.RepeatForever()
                            .WithIntervalInSeconds(5)//间隔5秒
                            .Build();
                    });
            });

            _jobManager.Start();
        }

        public void StartUp()
        {
            ToDoDayJob();
        }
    }
}
