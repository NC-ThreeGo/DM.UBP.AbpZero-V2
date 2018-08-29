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
using System.Threading;
using System.Threading.Tasks;

namespace DM.UBP.Console
{
    public class UBPConsoleApplication<TStartupModule> where TStartupModule : AbpModule
    {
        public static AbpBootstrapper AbpBootstrapper { get; } = AbpBootstrapper.Create<TStartupModule>();

        public void Application_Start()
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
               f => f.UseAbpLog4Net().WithConfig("log4net.config"));

            reInit:
            try
            {
                AbpBootstrapper.Initialize();
            }
            catch (Exception ex)
            {
                //可能因为default数据库未启动，这里会报错。需要等待30秒重新启动服务
                System.Console.WriteLine("AbpBootstrapper 加载失败！");
                Thread.Sleep(30000);
                //Application_Start();
                goto reInit;
            }



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
