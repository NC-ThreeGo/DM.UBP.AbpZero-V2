using Abp.AutoMapper;
using Abp.Modules;
using System.Reflection;

namespace DM.UBP.Application.Dto
{
    [DependsOn(typeof(AbpAutoMapperModule))]
    public class UbpApplicationDtoModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                //Add your custom AutoMapper mappings here...
                //mapper.CreateMap<Module, ModuleListDto>();
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
