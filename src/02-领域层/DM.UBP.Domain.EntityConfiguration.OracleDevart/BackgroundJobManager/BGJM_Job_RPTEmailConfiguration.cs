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
    public class Job_RPTEmailConfiguration : EntityConfigurationBase<Job_RPTEmail, long>
    {
        public Job_RPTEmailConfiguration()
        {
            this.ToTable("BGJM_JOB_RPTEMAIL");
            this.Property(p => p.Id).HasColumnName("JOB_RPTEMAIL_ID");
        }
    }
}
