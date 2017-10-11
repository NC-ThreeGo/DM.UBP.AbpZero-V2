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
    /// 报表参数基于数据库—OracleDevart的映射
    /// <summary>
    public class ReportParameterConfiguration : EntityConfigurationBase<ReportParameter, long>
    {
        public ReportParameterConfiguration()
        {
            this.ToTable("RPT_PARAMETER");
            this.Property(p => p.Id).HasColumnName("PARAMETER_ID");
        }
    }
}
