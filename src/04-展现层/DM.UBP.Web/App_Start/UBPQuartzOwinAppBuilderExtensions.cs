using Abp;
using Abp.Dependency;
using DM.UBP.Application.Quartz.Servers;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DM.UBP.Web
{
    public static class UBPQuartzOwinAppBuilderExtensions
    {
        public static void QuartzServerStartUp(this IAppBuilder app)
        {
            if (!IocManager.Instance.IsRegistered<IQuartzServer>())
            {
                throw new AbpException("IQuartzServer is not registered!");
            }

            var triggerServer = IocManager.Instance.Resolve<IQuartzServer>();

            if (ConfigurationManager.AppSettings["EnabledQuartzServer"] == null)
            {
                triggerServer.StartUp();
                return;
            }
            if (Boolean.Parse(ConfigurationManager.AppSettings["EnabledQuartzServer"]))
            {
                triggerServer.StartUp();
                return;
            }
        }
    }
}