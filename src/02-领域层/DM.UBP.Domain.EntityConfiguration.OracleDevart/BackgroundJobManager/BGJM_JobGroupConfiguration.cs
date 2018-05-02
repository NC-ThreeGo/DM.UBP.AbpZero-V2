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
    /// 工作组基于数据库—OracleDevart的映射
    /// <summary>
    public class JobGroupConfiguration : EntityConfigurationBase<JobGroup, long>
    {
        public JobGroupConfiguration()
        {
            this.ToTable("BGJM_JOBGROUP");
            this.Property(p => p.Id).HasColumnName("JOBGROUP_ID");
        }
    }
}
