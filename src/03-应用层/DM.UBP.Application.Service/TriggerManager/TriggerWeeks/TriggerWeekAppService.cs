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
using DM.UBP.Domain.Entity.TriggerManager;
using DM.UBP.Domain.Service.TriggerManager.TriggerWeeks;
using DM.UBP.Domain.Service.TriggerManager;
using DM.UBP.Application.Dto.TriggerManager.TriggerWeeks;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.TriggerManager.TriggerWeeks
{
    /// <summary>
    /// 按周计算的触发器的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_TriggerManager.Pages_TriggerManager_TriggerWeeks)]
    public class TriggerWeekAppService : ITriggerWeekAppService
    {
        private readonly ITriggerWeekManager _TriggerWeekManager;
        public TriggerWeekAppService(
           ITriggerWeekManager triggerweekmanager
           )
        {
            _TriggerWeekManager = triggerweekmanager;
        }

        public async Task<PagedResultDto<TriggerWeekOutputDto>> GetTriggerWeeks()
        {
            var entities = await _TriggerWeekManager.GetAllTriggerWeeksAsync();
            var listDto = entities.MapTo<List<TriggerWeekOutputDto>>();

            return new PagedResultDto<TriggerWeekOutputDto>(
            listDto.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<TriggerWeekOutputDto>> GetTriggerWeeks(PagedAndSortedInputDto input)
        {
            var entities = await _TriggerWeekManager.GetAllTriggerWeeksAsync();
            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";
            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));
            var listDto = pageEntities.MapTo<List<TriggerWeekOutputDto>>();

            return new PagedResultDto<TriggerWeekOutputDto>(
            entities.Count,
            listDto
            );
        }
        public async Task<TriggerWeekOutputDto> GetTriggerWeekById(int id)
        {
            var entity = await _TriggerWeekManager.GetTriggerWeekByIdAsync(id);
            return entity.MapTo<TriggerWeekOutputDto>();
        }
        [AbpAuthorize(AppPermissions_TriggerManager.Pages_TriggerManager_TriggerWeeks_Create)]
        public async Task<bool> CreateTriggerWeek(TriggerWeekInputDto input)
        {
            var entity = input.MapTo<TriggerWeek>();
            return await _TriggerWeekManager.CreateTriggerWeekAsync(entity);
        }
        [AbpAuthorize(AppPermissions_TriggerManager.Pages_TriggerManager_TriggerWeeks_Edit)]
        public async Task<bool> UpdateTriggerWeek(TriggerWeekInputDto input)
        {
            var entity = await _TriggerWeekManager.GetTriggerWeekByIdAsync(input.Id);
            input.MapTo(entity);
            return await _TriggerWeekManager.UpdateTriggerWeekAsync(entity);
        }
        [AbpAuthorize(AppPermissions_TriggerManager.Pages_TriggerManager_TriggerWeeks_Delete)]
        public async Task DeleteTriggerWeek(EntityDto input)
        {
            var entity = await _TriggerWeekManager.GetTriggerWeekByIdAsync(input.Id);
            await _TriggerWeekManager.DeleteTriggerWeekAsync(entity);
        }
    }
}
