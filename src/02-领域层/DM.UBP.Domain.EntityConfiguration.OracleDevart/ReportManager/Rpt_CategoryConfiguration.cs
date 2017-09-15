//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using DM.UBP.EF;
using DM.UBP.Domain.Entity.ReportManager;

namespace DM.UBP.Domain.EntityConfiguration.OracleDevart.ReportManager
{
    /// <summary>
    /// 报表分类基于数据库—OracleDevart的映射
    /// <summary>
    public class CategoryConfiguration : EntityConfigurationBase<ReportCategory, long>
    {
        public CategoryConfiguration()
        {
            this.ToTable("RPT_CATEGORY");
            this.Property(p => p.Id).HasColumnName("CATEGORY_ID");
        }
    }
}
