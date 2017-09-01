using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Common.Config
{
    /// <summary>
    /// Ubp数据配置信息重置类
    /// </summary>
    public interface IDbContextInitializerConfigReseter
    {
        /// <summary>
        /// 重置数据配置信息
        /// </summary>
        /// <param name="config">原始数据配置信息</param>
        /// <returns>重置后的数据配置信息</returns>
        DbContextInitializerConfig Reset(DbContextInitializerConfig config);
    }
}
