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
using DM.UBP.Domain.Service.BackgroundJobManager.Schedulers;
using DM.UBP.Domain.Service.BackgroundJobManager;
using DM.UBP.Application.Dto.BackgroundJobManager.Schedulers;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.BackgroundJobManager.Schedulers
{
    /// <summary>
    /// 工作的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Schedulers)]
    public class SchedulerAppService : ISchedulerAppService
    {
        private readonly ISchedulerManager _SchedulerManager;
        public SchedulerAppService(
           ISchedulerManager schedulermanager
           )
        {
            _SchedulerManager = schedulermanager;
        }

        public async Task<PagedResultDto<SchedulerOutputDto>> GetSchedulers()
        {
            var entities = await _SchedulerManager.GetAllSchedulersAsync();
            var listDto = entities.MapTo<List<SchedulerOutputDto>>();

            return new PagedResultDto<SchedulerOutputDto>(
            listDto.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<SchedulerOutputDto>> GetSchedulers(PagedAndSortedInputDto input)
        {
            var entities = await _SchedulerManager.GetAllSchedulersAsync();
            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";
            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));
            var listDto = pageEntities.MapTo<List<SchedulerOutputDto>>();

            return new PagedResultDto<SchedulerOutputDto>(
            entities.Count,
            listDto
            );
        }
        public async Task<SchedulerOutputDto> GetSchedulerById(long id)
        {
            var entity = await _SchedulerManager.GetSchedulerByIdAsync(id);
            return entity.MapTo<SchedulerOutputDto>();
        }
        [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Schedulers_Create)]
        public async Task<bool> CreateScheduler(SchedulerInputDto input)
        {
            var entity = input.MapTo<Scheduler>();
            return await _SchedulerManager.CreateSchedulerAsync(entity);
        }
        [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Schedulers_Edit)]
        public async Task<bool> UpdateScheduler(SchedulerInputDto input)
        {
            var entity = await _SchedulerManager.GetSchedulerByIdAsync(input.Id);
            input.MapTo(entity);
            return await _SchedulerManager.UpdateSchedulerAsync(entity);
        }
        [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Schedulers_Delete)]
        public async Task DeleteScheduler(EntityDto input)
        {
            var entity = await _SchedulerManager.GetSchedulerByIdAsync(input.Id);
            await _SchedulerManager.DeleteSchedulerAsync(entity);
        }
    }
}
