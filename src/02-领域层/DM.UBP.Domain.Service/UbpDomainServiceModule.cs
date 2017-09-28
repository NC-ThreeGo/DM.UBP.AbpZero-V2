using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Localization.Sources;
using Abp.Modules;
using DM.UBP.Domain.Entity;
using System.Reflection;

namespace DM.UBP.Domain.Service
{
    [DependsOn(
        typeof(UBPCoreModule),
        typeof(UbpDomainEntityModule)
        )]
    public class UbpDomainServiceModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Add/remove localization sources here
            //加载自己的多语言文件
            Configuration.Localization.Sources.Extensions.Add(
                new LocalizationSourceExtensionInfo(
                    UBPConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(), "DM.UBP.Domain.Service.Localization.UBP"
                    ))
                );

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void Initialize()
        {
            
        }

        public override void PostInitialize()
        {
        }
    }
}
