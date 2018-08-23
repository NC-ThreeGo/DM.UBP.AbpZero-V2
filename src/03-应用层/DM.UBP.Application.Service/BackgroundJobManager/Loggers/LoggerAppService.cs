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
using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Authorization;
using Abp.Application.Services.Dto;
using DM.UBP.Domain.Entity.BackgroundJobManager;
using DM.UBP.Domain.Service.BackgroundJobManager.Loggers;
using DM.UBP.Domain.Service.BackgroundJobManager;
using DM.UBP.Application.Dto.BackgroundJobManager.Loggers;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.BackgroundJobManager.Loggers
{
    /// <summary>
    /// 后台任务系统日志的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Loggers)]
    public class LoggerAppService : ILoggerAppService
    {
        private readonly ILoggerManager _LoggerManager;
        public LoggerAppService(
           ILoggerManager loggermanager
           )
        {
            _LoggerManager = loggermanager;
        }

        public async Task<PagedResultDto<LoggerOutputDto>> GetLoggers()
        {
            var entities = await _LoggerManager.GetAllLoggersAsync();
            var listDto = entities.MapTo<List<LoggerOutputDto>>();

            return new PagedResultDto<LoggerOutputDto>(
            listDto.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<LoggerOutputDto>> GetLoggers(PagedAndSortedInputDto input)
        {
            var entities = await _LoggerManager.GetAllLoggersAsync();
            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";
            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));
            var listDto = pageEntities.MapTo<List<LoggerOutputDto>>();

            return new PagedResultDto<LoggerOutputDto>(
            entities.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<LoggerOutputDto>> GetLoggers(LoggerFilterDto input)
        {
            var entities = await _LoggerManager.GetAllLoggersAsync();

            //执行开始时间或者结束时间在  选择的时间范围之内的
            entities = entities.Where(s=> 
            (s.ExecStartTime > input.StartDate && s.ExecStartTime <= input.EndDate) ||
            (s.ExecEndTime > input.StartDate && s.ExecEndTime <= input.EndDate)
            ).ToList();

            if (!string.IsNullOrEmpty(input.Filter))
                entities = entities.Where(s => s.JobName.Contains(input.Filter.Trim())).ToList();

            if(input.IsException != null)
                entities = entities.Where(s => s.IsException == input.IsException).ToList();

            if(!string.IsNullOrEmpty(input.JobType))
                entities = entities.Where(s => s.JobType == input.JobType).ToList();

            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "isexception desc ,id desc";

            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));
            var listDto = pageEntities.MapTo<List<LoggerOutputDto>>();

            return new PagedResultDto<LoggerOutputDto>(
            entities.Count,
            listDto
            );
        }
        public async Task<LoggerOutputDto> GetLoggerById(long id)
        {
            var entity = await _LoggerManager.GetLoggerByIdAsync(id);
            return entity.MapTo<LoggerOutputDto>();
        }

        public async Task<bool> CreateLogger(LoggerInputDto input)
        {
            var entity = input.MapTo<Logger>();
            return await _LoggerManager.CreateLoggerAsync(entity);
        }

        public async Task<bool> UpdateLogger(LoggerInputDto input)
        {
            var entity = await _LoggerManager.GetLoggerByIdAsync(input.Id);
            input.MapTo(entity);
            return await _LoggerManager.UpdateLoggerAsync(entity);
        }

        public async Task DeleteLogger(EntityDto input)
        {
            var entity = await _LoggerManager.GetLoggerByIdAsync(input.Id);
            await _LoggerManager.DeleteLoggerAsync(entity);
        }
    }
}
