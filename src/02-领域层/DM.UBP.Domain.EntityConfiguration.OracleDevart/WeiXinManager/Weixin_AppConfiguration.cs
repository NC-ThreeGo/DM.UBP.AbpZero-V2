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
    public class WeiXinAppConfiguration : EntityConfigurationBase<WeiXinApp, long>
    {
        public WeiXinAppConfiguration()
        {
            this.ToTable("WEIXIN_APP");
            this.Property(p => p.Id).HasColumnName("APP_ID");
            this.Property(p => p.Descriotion).HasColumnName("DESCRIPTION");
            this.Property(p => p.Redorect_Domain).HasColumnName("REDIRECT_DOMAIN");
        }
    }
}
