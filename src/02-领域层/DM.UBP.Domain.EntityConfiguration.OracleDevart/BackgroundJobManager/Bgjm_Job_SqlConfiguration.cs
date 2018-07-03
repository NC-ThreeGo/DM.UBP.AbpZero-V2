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
    /// SQL任务基于数据库—OracleDevart的映射
    /// <summary>
    public class Job_SqlConfiguration : EntityConfigurationBase<Job_Sql, long>
    {
        public Job_SqlConfiguration()
        {
            this.ToTable("BGJM_JOB_SQL");
            this.Property(p => p.Id).HasColumnName("JOB_SQL_ID");
        }
    }
}
