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
using DM.UBP.Domain.Service.TriggerManager.TriggerDays;
using DM.UBP.Domain.Service.TriggerManager;
using DM.UBP.Application.Dto.TriggerManager.TriggerDays;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.TriggerManager.TriggerDays
{
    /// <summary>
    /// 按天计算的触发器的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_TriggerManager.Pages_TriggerManager_TriggerDays)]
    public class TriggerDayAppService : ITriggerDayAppService
    {
        private readonly ITriggerDayManager _TriggerDayManager;
        public TriggerDayAppService(
           ITriggerDayManager triggerdaymanager
           )
        {
            _TriggerDayManager = triggerdaymanager;
        }

        public async Task<PagedResultDto<TriggerDayOutputDto>> GetTriggerDays()
        {
            var entities = await _TriggerDayManager.GetAllTriggerDaysAsync();
            var listDto = entities.MapTo<List<TriggerDayOutputDto>>();

            return new PagedResultDto<TriggerDayOutputDto>(
            listDto.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<TriggerDayOutputDto>> GetTriggerDays(PagedAndSortedInputDto input)
        {
            var entities = await _TriggerDayManager.GetAllTriggerDaysAsync();
            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";
            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));
            var listDto = pageEntities.MapTo<List<TriggerDayOutputDto>>();

            return new PagedResultDto<TriggerDayOutputDto>(
            entities.Count,
            listDto
            );
        }
        public async Task<TriggerDayOutputDto> GetTriggerDayById(int id)
        {
            var entity = await _TriggerDayManager.GetTriggerDayByIdAsync(id);
            return entity.MapTo<TriggerDayOutputDto>();
        }
        [AbpAuthorize(AppPermissions_TriggerManager.Pages_TriggerManager_TriggerDays_Create)]
        public async Task<bool> CreateTriggerDay(TriggerDayInputDto input)
        {
            var entity = input.MapTo<TriggerDay>();
            return await _TriggerDayManager.CreateTriggerDayAsync(entity);
        }
        [AbpAuthorize(AppPermissions_TriggerManager.Pages_TriggerManager_TriggerDays_Edit)]
        public async Task<bool> UpdateTriggerDay(TriggerDayInputDto input)
        {
            var entity = await _TriggerDayManager.GetTriggerDayByIdAsync(input.Id);
            input.MapTo(entity);
            return await _TriggerDayManager.UpdateTriggerDayAsync(entity);
        }
        [AbpAuthorize(AppPermissions_TriggerManager.Pages_TriggerManager_TriggerDays_Delete)]
        public async Task DeleteTriggerDay(EntityDto input)
        {
            var entity = await _TriggerDayManager.GetTriggerDayByIdAsync(input.Id);
            await _TriggerDayManager.DeleteTriggerDayAsync(entity);
        }
    }
}
