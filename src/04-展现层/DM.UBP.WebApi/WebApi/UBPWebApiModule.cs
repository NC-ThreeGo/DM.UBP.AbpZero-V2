using System.Linq;
using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Swashbuckle.Application;
using DM.UBP.Application.Service;
using DM.UBP.Application.Quartz;
using DM.UBP.Application.Quartz.Servers;

namespace DM.UBP.WebApi
{
    /// <summary>
    /// Web API layer of the application.
    /// </summary>
    [DependsOn(typeof(AbpWebApiModule), typeof(UBPApplicationModule))]
    [DependsOn(typeof(UbpApplicationServiceModule), typeof(UbpAppQuartzModule))]
    public class UBPWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //Automatically creates Web API controllers for all application services of the application
            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(UBPApplicationModule).Assembly, "app")
                .Build();

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(UbpApplicationServiceModule).Assembly, "ubp")
                .Build();

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
               .ForAll<IApplicationService>(typeof(UbpAppQuartzModule).Assembly, "quartz")
               .WithConventionalVerbs()
               .Build();


            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));

            ConfigureSwaggerUi(); //Remove this line to disable swagger UI.
        }

        private void ConfigureSwaggerUi()
        {
            Configuration.Modules.AbpWebApi().HttpConfiguration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "DM.UBP.WebApi");
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                })
                .EnableSwaggerUi(c =>
                {
                    c.InjectJavaScript(Assembly.GetAssembly(typeof(UBPWebApiModule)), "DM.UBP.WebApi.Scripts.Swagger-Custom.js");
                });
        }
    }
}
