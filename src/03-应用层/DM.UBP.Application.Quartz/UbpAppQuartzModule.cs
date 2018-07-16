using Abp.Dependency;
using Abp.Modules;
using Abp.Quartz.Quartz;
using Abp.Quartz.Quartz.Configuration;
using DM.UBP.Application.Quartz.Managers;
using DM.UBP.Application.Quartz.Servers;
using DM.UBP.Domain.Service;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Quartz
{
    [DependsOn(typeof(AbpQuartzModule),
        typeof(UbpDomainServiceModule),
        typeof(UBPApplicationModule))]
    public class UbpAppQuartzModule: AbpModule
    {
        public override void PreInitialize()
        {
            //IocManager.Register<IQuartzServer, QuartzServer>();
            //IocManager.Register<IUBPQuartzScheduleJobManager, UBPQuartzScheduleJobManager>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            //if (IocManager.IsRegistered<IJobListener>())
            //{
            //    IocManager.Release(IocManager.Resolve<IJobListener>());
            //}
            //IocManager.RegisterIfNot<IJobListener, UBPQuartzJobListener>();

            ///删除ABP的JobListener 删除ABP的Listener 在UBP的JobListener 用Logger写日志失败
            //Configuration.Modules.AbpQuartz().Scheduler.ListenerManager.RemoveJobListener("AbpJobListener");

            ///使用UBP的JobListener
            Configuration.Modules.AbpQuartz().Scheduler.ListenerManager.AddJobListener(new UBPQuartzJobListener());

        }
    }
}
