using System;
using System.Configuration;

namespace DM.UBP.Common.Config
{
    /// <summary>
    /// Ubp配置文件对应的配置类
    /// </summary>
    public sealed class UbpConfig
    {
        private const string UbpSectionName = "ubp";
        private static readonly Lazy<UbpConfig> InstanceLazy
            = new Lazy<UbpConfig>(() => new UbpConfig());

        /// <summary>
        /// 初始化一个新的<see cref="OSharpConfig"/>实例
        /// </summary>
        private UbpConfig()
        {
            UbpConfigSection section = (UbpConfigSection)ConfigurationManager.GetSection(UbpSectionName);
            if (section == null)
            {
                DbContextInitializerConfig = new DbContextInitializerConfig();
                AbpZeroDbMigratorConfig = new AbpZeroDbMigratorConfig();
                return;
            }
            DbContextInitializerConfig = new DbContextInitializerConfig(section.DbContextInitializer);
            AbpZeroDbMigratorConfig = new AbpZeroDbMigratorConfig(section.AbpZeroDbMigrator);
        }

        /// <summary>
        /// 获取 配置类的单一实例
        /// </summary>
        public static UbpConfig Instance
        {
            get
            {
                UbpConfig config = InstanceLazy.Value;
                if (DbContextInitializerConfigReseter != null)
                {
                    config.DbContextInitializerConfig = DbContextInitializerConfigReseter.Reset(config.DbContextInitializerConfig);
                }
                return config;
            }
        }

        /// <summary>
        /// 获取或设置 数据配置重置信息
        /// </summary>
        public static IDbContextInitializerConfigReseter DbContextInitializerConfigReseter { get; set; }

        /// <summary>
        /// 获取或设置 数据配置信息
        /// </summary>
        public DbContextInitializerConfig DbContextInitializerConfig { get; set; }

        /// <summary>
        /// 获取或设置 AbpZeroDbMigrator配置信息
        /// </summary>
        public AbpZeroDbMigratorConfig AbpZeroDbMigratorConfig { get; set; }
    }
}
