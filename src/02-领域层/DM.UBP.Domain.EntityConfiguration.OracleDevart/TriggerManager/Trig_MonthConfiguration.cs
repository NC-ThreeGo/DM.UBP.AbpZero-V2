//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using DM.UBP.EF;
using DM.UBP.Domain.Entity.TriggerManager;

namespace DM.UBP.Domain.EntityConfiguration.OracleDevart.TriggerManager
{
    /// <summary>
    /// 按月计算的触发器基于数据库—OracleDevart的映射
    /// <summary>
    public class TriggerMonthConfiguration : EntityConfigurationBase<TriggerMonth, int>
    {
        public TriggerMonthConfiguration()
        {
            this.ToTable("TRIG_MONTH");
            this.Property(p => p.Id).HasColumnName("MONTH_ID");
        }
    }
}
