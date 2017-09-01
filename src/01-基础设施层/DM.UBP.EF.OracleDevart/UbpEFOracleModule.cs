using Abp.Modules;
using System.Reflection;
using TG.UBP.EF;

namespace TG.UBP
{
    [DependsOn(typeof(UbpEFModule))]
    public class UbpEFOracleModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
