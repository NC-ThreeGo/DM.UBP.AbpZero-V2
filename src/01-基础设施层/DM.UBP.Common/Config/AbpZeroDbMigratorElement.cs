using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Common.Config
{
    public class AbpZeroDbMigratorElement : ConfigurationElement
    {
        private const string TypeKey = "type";

        /// <summary>
        /// 获取或设置 AbpZeroDbMigrator类型名称
        /// </summary>
        [ConfigurationProperty(TypeKey, IsRequired = true)]
        public virtual string AbpZeroDbMigratorTypeName
        {
            get { return (string)this[TypeKey]; }
            set { this[TypeKey] = value; }
        }
    }
}
