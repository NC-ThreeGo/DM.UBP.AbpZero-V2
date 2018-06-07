using Abp;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Quartz.Quartz;
using Abp.Runtime.Caching.Redis;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Abp.Zero;
using Abp.Zero.Ldap;
using Castle.MicroKernel.Registration;
using DM.UBP.Application.Quartz;
using DM.UBP.Application.Quartz.Servers;
using DM.UBP.Application.Service;
using DM.UBP.Domain.Service;
using DM.UBP.EF;
using DM.UBP.WebApi;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Console
{
    [DependsOn(typeof(UbpEFModule), typeof(UbpAppQuartzModule))]
    public class UBPConsoleModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            
            //IocManager.IocContainer.Register(
            //    Component
            //        .For<IAuthenticationManager>()
            //        .LifestyleTransient()
            //    );

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            //if (!Abp.Dependency.IocManager.Instance.IsRegistered<IQuartzServer>())
            //{
            //    throw new AbpException("IQuartzServer is not registered!");
            //}

            //var triggerServer = Abp.Dependency.IocManager.Instance.Resolve<IQuartzServer>();

            //triggerServer.StartUp();
        }
    }
}
