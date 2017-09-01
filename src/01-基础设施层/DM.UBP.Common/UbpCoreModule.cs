using Abp;
using Abp.Modules;
using System.Reflection;

namespace DM.UBP.Common
{
    [DependsOn(typeof(AbpKernelModule))]
    public class UbpCommonModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());//这里，进行依赖注入的注册。
        }
    }
}
