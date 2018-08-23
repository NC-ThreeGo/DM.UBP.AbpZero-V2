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
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DM.UBP.Application.Dto.BackgroundJobManager.Loggers;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.BackgroundJobManager.Loggers
{
    /// <summary>
    /// 后台任务系统日志的Application.Service.Interface
    /// <summary>
    public interface ILoggerAppService : IApplicationService
    {
        Task<PagedResultDto<LoggerOutputDto>> GetLoggers();

        Task<PagedResultDto<LoggerOutputDto>> GetLoggers(PagedAndSortedInputDto input);

        Task<PagedResultDto<LoggerOutputDto>> GetLoggers(LoggerFilterDto input);

        Task<LoggerOutputDto> GetLoggerById(long id);

        Task<bool> CreateLogger(LoggerInputDto input);

        Task<bool> UpdateLogger(LoggerInputDto input);

        Task DeleteLogger(EntityDto input);

    }
}
