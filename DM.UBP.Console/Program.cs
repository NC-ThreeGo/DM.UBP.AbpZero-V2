using Abp;
using Abp.Castle.Logging.Log4Net;
using Abp.Dependency;
using Castle.Facilities.Logging;
using DM.UBP.Application.Quartz.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Console
{
    class Program : UBPConsoleModule
    {
        static void Main(string[] args)
        {
            Console_Start();
        }

        private static void Console_Start()
        {
            var ubpConsoleModule = AbpBootstrapper.Create<UBPConsoleModule>();
            ubpConsoleModule.IocManager.IocContainer
                .AddFacility<LoggingFacility>(f => f.UseAbpLog4Net().WithConfig("log4net.config"));
            ubpConsoleModule.Initialize();
        }
    }
}
