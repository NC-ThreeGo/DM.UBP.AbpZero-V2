//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using DM.UBP.EF;
using DM.UBP.Domain.Entity.BackgroundJobManager;

namespace DM.UBP.Domain.EntityConfiguration.OracleDevart.BackgroundJobManager
{
    /// <summary>
    /// 工作基于数据库—OracleDevart的映射
    /// <summary>
    public class TriggerConfiguration : EntityConfigurationBase<Trigger, long>
    {
        public TriggerConfiguration()
        {
            this.ToTable("BGJM_TRIGGER");
            this.Property(p => p.Id).HasColumnName("TRIGGER_ID");
        }
    }
}
