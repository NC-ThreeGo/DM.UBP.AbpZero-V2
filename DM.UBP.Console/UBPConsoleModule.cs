using Abp;
using Abp.Modules;
using DM.UBP.Application.Quartz;
using DM.UBP.Application.Quartz.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Console
{
    [DependsOn(
        typeof(UbpAppQuartzModule)
        )]
    public class UBPConsoleModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            
        }

        public override void PostInitialize()
        {
            if (!Abp.Dependency.IocManager.Instance.IsRegistered<IQuartzServer>())
            {
                throw new AbpException("IQuartzServer is not registered!");
            }

            var triggerServer = Abp.Dependency.IocManager.Instance.Resolve<IQuartzServer>();

            triggerServer.StartUp();
        }
    }
}
