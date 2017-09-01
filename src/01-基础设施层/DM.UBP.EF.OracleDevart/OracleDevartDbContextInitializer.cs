using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration.Startup;
using System.Transactions;

namespace DM.UBP.EF.OracleDevart
{
    /// <summary>
    /// 基于Devart的Oracle数据上下文初始化
    /// </summary>
    public class OracleDevartDbContextInitializer : DbContextInitializerBase<UbpDbContext>
    {
        private bool _IgnoreSchemaName = true;
        private bool _DisableQuoting = true;
        private bool _UseDateTimeAsDate = true;
        private bool _UseNonLobStrings = true;
        private bool _UseNonUnicodeStrings = true;
        private bool _TruncateLongDefaultNames = true;

        private bool _RunOracleMonitor = true;

        /// <summary>
        /// 数据库访问时，是否忽略Schema
        /// </summary>
        [DefaultValue(true)]
        public bool IgnoreSchemaName
        {
            get { return _IgnoreSchemaName; }
            set { _IgnoreSchemaName = value; }
        }

        /// <summary>
        /// 数据库访问时，是否忽略单引号
        /// </summary>
        [DefaultValue(true)]
        public bool DisableQuoting
        {
            get { return _DisableQuoting; }
            set { _DisableQuoting = value; }
        }

        /// <summary>
        /// Devart默认将DateTime映射成TIMESTAMP类型，需要映射成Date
        /// </summary>
        [DefaultValue(true)]
        public bool UseDateTimeAsDate
        {
            get { return _UseDateTimeAsDate; }
            set { _UseDateTimeAsDate = value; }
        }

        /// <summary>
        /// Devart默认将没有长度限制的String映射成Lob类型，需要映射成varchar2
        /// </summary>
        [DefaultValue(true)]
        public bool UseNonLobStrings
        {
            get { return _UseNonLobStrings; }
            set { _UseNonLobStrings = value; }
        }

        /// <summary>
        /// Devart默认使用Unicode类型的字符，但由于Devart设置的MaxStringSize是4000，
        ///     所以会导致Nvchar2(4000)超出Oracle允许的最大长度，如果不使用Unicode，则Devart映射成Varchar2(4000),可以通过。
        /// </summary>
        [DefaultValue(true)]
        public bool UseNonUnicodeStrings
        {
            get { return _UseNonUnicodeStrings; }
            set { _UseNonUnicodeStrings = value; }
        }

        /// <summary>
        /// 是否对超长的默认名进行截断，有时候Devart生成的Oracle对象会超出允许长度，需要截断。
        /// </summary>
        [DefaultValue(true)]
        public bool TruncateLongDefaultNames
        {
            get { return _TruncateLongDefaultNames; }
            set { _TruncateLongDefaultNames = value; }
        }

        /// <summary>
        /// 在数据库初始化时，是否运行OracleMonitor
        /// </summary>
        [DefaultValue(true)]
        public bool RunOracleMonitor
        {
            get { return _RunOracleMonitor; }
            set { _RunOracleMonitor = value; }
        }

        /// <summary>
        /// 初始化一个<see cref="OracleDefaultDbContextInitializer"/>新实例
        /// </summary>
        public OracleDevartDbContextInitializer()
        {
            InitDevart();

            DatabaseInitializer = new OracleDevartCreateDatabaseIfNotExistsWithSeed();
            if (EnabledAutoMigrate)
            {
                DatabaseInitializer = new MigrateDatabaseToLatestVersion<UbpDbContext, OracleDevartMigrationsConfiguration>();
            }

            //由于OracleDevart不支持默认的ReadUncommitted事物级别，需要修改成ReadCommitted。
            AbpConfiguration.UnitOfWork.IsolationLevel = IsolationLevel.ReadCommitted;

            //Devart和DynamicFilter v1.4.10以上版本不兼容（启用动态过滤器后，Sql参数不对），需要增加下面代码修复。
            //  http://forums.devart.com/viewtopic.php?f=1&t=35458#p123206
            System.Data.Entity.Infrastructure.Interception.DbInterception.Add(new CommandInterceptor());
        }

        /// <summary>
        /// 对Devart的全局配置进行初始化。
        /// </summary>
        private void InitDevart()
        {
            if (ConfigurationManager.AppSettings["IgnoreSchemaName"] != null)
            {
                _IgnoreSchemaName = Boolean.Parse(ConfigurationManager.AppSettings["IgnoreSchemaName"]);
            }

            if (ConfigurationManager.AppSettings["DisableQuoting"] != null)
            {
                _DisableQuoting = Boolean.Parse(ConfigurationManager.AppSettings["DisableQuoting"]);
            }

            if (ConfigurationManager.AppSettings["UseDateTimeAsDate"] != null)
            {
                _UseDateTimeAsDate = Boolean.Parse(ConfigurationManager.AppSettings["UseDateTimeAsDate"]);
            }

            if (ConfigurationManager.AppSettings["UseNonLobStrings"] != null)
            {
                _UseNonLobStrings = Boolean.Parse(ConfigurationManager.AppSettings["UseNonLobStrings"]);
            }

            if (ConfigurationManager.AppSettings["UseNonUnicodeStrings"] != null)
            {
                _UseNonUnicodeStrings = Boolean.Parse(ConfigurationManager.AppSettings["UseNonUnicodeStrings"]);
            }

            if (ConfigurationManager.AppSettings["TruncateLongDefaultNames"] != null)
            {
                _TruncateLongDefaultNames = Boolean.Parse(ConfigurationManager.AppSettings["TruncateLongDefaultNames"]);
            }

            if (ConfigurationManager.AppSettings["RunOracleMonitor"] != null)
            {
                _RunOracleMonitor = Boolean.Parse(ConfigurationManager.AppSettings["RunOracleMonitor"]);
            }

            if (RunOracleMonitor)
            {
                // We have turned on OracleMonitor to view executed DDL и DML statements in Devart dbMonitor application
                Devart.Data.Oracle.OracleMonitor monitor = new Devart.Data.Oracle.OracleMonitor() { IsActive = true };
            }

            //--------------------------------------------------------------
            // You use the capability for configuring the behavior of the EF-provider:
            Devart.Data.Oracle.Entity.Configuration.OracleEntityProviderConfig devartConfig = Devart.Data.Oracle.Entity.Configuration.OracleEntityProviderConfig.Instance;
            // Now, you switch off schema name generation while generating DDL scripts and DML:
            devartConfig.Workarounds.IgnoreSchemaName = IgnoreSchemaName;
            devartConfig.Workarounds.DisableQuoting = DisableQuoting;
            devartConfig.CodeFirstOptions.UseDateTimeAsDate = UseDateTimeAsDate;
            devartConfig.CodeFirstOptions.UseNonLobStrings = UseNonLobStrings;
            devartConfig.CodeFirstOptions.UseNonUnicodeStrings = UseNonUnicodeStrings;
            devartConfig.CodeFirstOptions.TruncateLongDefaultNames = TruncateLongDefaultNames;
            //devartConfig.DatabaseScript.Column.MaxStringSize = Devart.Data.Oracle.Entity.Configuration.OracleMaxStringSize.Standard;
        }
    }
}
