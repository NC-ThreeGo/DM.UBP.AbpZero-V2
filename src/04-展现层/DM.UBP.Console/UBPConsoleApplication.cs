using Abp;
using Abp.Castle.Logging.Log4Net;
using Abp.Modules;
using Castle.Facilities.Logging;
using DM.UBP.Application.Quartz.Servers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Console
{
    public class UBPConsoleApplication<TStartupModule> where TStartupModule : AbpModule
    {
        public static AbpBootstrapper AbpBootstrapper { get; } = AbpBootstrapper.Create<TStartupModule>();

        public void Application_Start()
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig("log4net.config")
            );

            AbpBootstrapper.Initialize();

            if (!Abp.Dependency.IocManager.Instance.IsRegistered<IQuartzServer>())
            {
                throw new AbpException("IQuartzServer is not registered!");
            }

            var triggerServer = Abp.Dependency.IocManager.Instance.Resolve<IQuartzServer>();

            triggerServer.StartUp();
        }

        public void Application_End()
        {
            AbpBootstrapper.Dispose();
        }
    }
}
