//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Domain.Services;
using DM.UBP.Domain.Entity.BackgroundJobManager;

namespace DM.UBP.Domain.Service.BackgroundJobManager.Loggers
{
    /// <summary>
    /// 后台任务系统日志的Domain.Service.Interface
    /// <summary>
    public interface ILoggerManager : IDomainService
    {
        Task<List<Logger>> GetAllLoggersAsync();

        Task<Logger> GetLoggerByIdAsync(long id);

        Task<bool> CreateLoggerAsync(Logger logger);

        Task<long> CreateLoggerToGetIdAsync(Logger logger);

        Task<bool> UpdateLoggerAsync(Logger logger);

        Task DeleteLoggerAsync(Logger logger);

    }
}
