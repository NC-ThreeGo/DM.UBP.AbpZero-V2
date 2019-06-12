using System.Reflection;
using Abp.AutoMapper;
using Abp.Localization;
using Abp.Modules;
using DM.UBP.Authorization;
using DM.UBP.Authorization.Users;
using DM.UBP.Organizations;

namespace DM.UBP
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(typeof(UBPCoreModule))]
    public class UBPApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper mappings
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                CustomDtoMapper.CreateMappings(mapper);
            });
        }

        public override void Initialize()
        {
            IocManager.Register<WX_IUserAppService, WX_UserAppService>();
            IocManager.Register<WX_IOrganizationUnitAppService, WX_OrganizationUnitAppService>();

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
