using Abp.Modules;
using Abp.Quartz.Quartz;
using DM.UBP.Application.Quartz.Managers;
using DM.UBP.Application.Quartz.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Quartz
{
    [DependsOn(typeof(AbpQuartzModule))]
    public class UbpAppQuartzModule: AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<ITriggerServer, TriggerServer>();
            IocManager.Register<IUBPQuartzScheduleJobManager, UBPQuartzScheduleJobManager>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
