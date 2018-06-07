using Abp.Modules;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using Castle.MicroKernel.Registration;
using DM.UBP.Common;
using DM.UBP.Common.Config;
using System.Reflection;

namespace DM.UBP.EF
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), 
                typeof(UbpCommonModule), 
                typeof(UBPCoreModule))]
    public class UbpEFModule : AbpModule
    {
        private static bool _databaseInitialized;

        public override void PreInitialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //Configuration.DefaultNameOrConnectionString = "default";

            //根据ubp配置，获取connectionStringName
            UbpConfig config = UbpConfig.Instance;
            Configuration.DefaultNameOrConnectionString = config.DbContextInitializerConfig.ConnectionStringName;
        }

        public override void Initialize()
        {
            //模板自带的代码，改为根据配置文件初始化数据库。
            //Database.SetInitializer<UbpDbContext>(null);

            //根据ubp配置，获取DbContextInitializer，并初始化
            UbpConfig config = UbpConfig.Instance;

            //根据ubp配置，获得基于当前数据库的IAbpZeroDbMigrator实现类，并向IocManager注册。
            IocManager.IocContainer.Register(Component.For(typeof(IAbpZeroDbMigrator)).ImplementedBy(config.AbpZeroDbMigratorConfig.AbpZeroDbMigratorType));

            IDatabaseInitializer databaseInitializer = IocManager.Resolve<IDatabaseInitializer>();
            if (!_databaseInitialized && databaseInitializer != null)
            {
                databaseInitializer.Initialize(config.DbContextInitializerConfig);
                _databaseInitialized = true;
            }
        }
    }
}
