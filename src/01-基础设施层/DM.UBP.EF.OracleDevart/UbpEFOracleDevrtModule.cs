using Abp.EntityFramework;
using Abp.Modules;
using System.Reflection;

namespace DM.UBP.EF
{
    [DependsOn(typeof(AbpEntityFrameworkModule))]
    public class UbpEFOracleDevrtModule : AbpModule
    {
        private static bool _databaseInitialized;

        public override void PreInitialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void Initialize()
        {
        }
    }
}
