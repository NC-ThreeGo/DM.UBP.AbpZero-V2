using System.Data.Entity;
using System.Reflection;
using Abp.Events.Bus;
using Abp.Modules;
using Castle.MicroKernel.Registration;
using DM.UBP.EntityFramework;

namespace DM.UBP.Migrator
{
    [DependsOn(typeof(UBPDataModule))]
    public class UBPMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<UBPDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(typeof(IEventBus), () =>
            {
                IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                );
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}