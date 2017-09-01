using Abp.AutoMapper;
using Abp.Modules;
using DM.UBP.Application.Dto;
using DM.UBP.Domain.Service;
using System.Reflection;

namespace DM.UBP.Application.Service
{
    [DependsOn(
        typeof(UbpDomainServiceModule), 
        typeof(UbpApplicationDtoModule),
        typeof(UBPApplicationModule)
        )]
    public class UbpApplicationServiceModule : AbpModule
    {
        public override void PreInitialize()
        {
            //注册Ubp Authorization Providers
            Configuration.Authorization.Providers.Add<UbpAuthorizationProvider>();

            //Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            //{
            //    CustomDtoMapper.CreateMappings(mapper);
            //});
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
