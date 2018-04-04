using Abp;
using Abp.Dependency;
using DM.UBP.Application.Quartz.Servers;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DM.UBP.Web
{
    public static class UBPQuartzOwinAppBuilderExtensions
    {
        public static void QuartzServerStartUp(this IAppBuilder app)
        {
            if (!IocManager.Instance.IsRegistered<ITriggerServer>())
            {
                throw new AbpException("ITriggerServer is not registered!");
            }

            var triggerServer = IocManager.Instance.Resolve<ITriggerServer>();

            triggerServer.StartUp();

        }
    }
}