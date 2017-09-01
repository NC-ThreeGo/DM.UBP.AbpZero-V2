using DM.Common.Extensions;
using DM.UBP.Common.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Common.Config
{
    public class AbpZeroDbMigratorConfig
    {
        public AbpZeroDbMigratorConfig()
        {

        }
        internal AbpZeroDbMigratorConfig(AbpZeroDbMigratorElement element)
        {
            Type type = Type.GetType(element.AbpZeroDbMigratorTypeName);
            if (type == null)
            {
                throw new InvalidOperationException(Resources.DbContextInitializerConfig_InitializerNotExists.FormatWith(element.AbpZeroDbMigratorTypeName));
            }
            AbpZeroDbMigratorType = type;
        }

        /// <summary>
        /// 获取或设置 AbpZeroDbMigrator类型
        /// </summary>
        public Type AbpZeroDbMigratorType { get; set; }
    }
}
