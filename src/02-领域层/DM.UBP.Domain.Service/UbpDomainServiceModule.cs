using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
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
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    //UBPConsts.LocalizationSourceName,
                    UbpDomainServiceConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "DM.UBP.Domain.Service.Localization.UBP"
                        )
                    )
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
        }
    }
}
