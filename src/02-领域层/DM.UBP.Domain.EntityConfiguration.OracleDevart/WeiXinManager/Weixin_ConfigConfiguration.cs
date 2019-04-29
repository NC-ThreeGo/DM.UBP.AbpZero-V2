//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using DM.UBP.EF;
using DM.UBP.Domain.Entity.WeiXinManager;

namespace DM.UBP.Domain.EntityConfiguration.OracleDevart.WeiXinManager
{
    /// <summary>
    /// 基于数据库—OracleDevart的映射
    /// <summary>
    public class WeiXinConfigConfiguration : EntityConfigurationBase<WeiXinConfig, long>
    {
        public WeiXinConfigConfiguration()
        {
            this.ToTable("WEIXIN_CONFIG");
            this.Property(p => p.Id).HasColumnName("CONFIG_ID");
        }
    }
}
