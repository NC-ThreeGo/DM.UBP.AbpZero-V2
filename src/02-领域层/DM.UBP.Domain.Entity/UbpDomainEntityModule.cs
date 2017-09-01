using Abp.Modules;
using Abp.Zero;
using System.Reflection;

namespace DM.UBP.Domain.Entity
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class UbpDomainEntityModule : AbpModule
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
