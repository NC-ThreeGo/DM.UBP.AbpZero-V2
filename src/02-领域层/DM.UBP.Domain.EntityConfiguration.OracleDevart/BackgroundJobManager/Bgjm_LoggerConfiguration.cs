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
    /// 后台任务系统日志基于数据库—OracleDevart的映射
    /// <summary>
    public class LoggerConfiguration : EntityConfigurationBase<Logger, long>
    {
        public LoggerConfiguration()
        {
            this.ToTable("BGJM_LOGGER");
            this.Property(p => p.Id).HasColumnName("LOG_ID");
            
        }
    }
}
