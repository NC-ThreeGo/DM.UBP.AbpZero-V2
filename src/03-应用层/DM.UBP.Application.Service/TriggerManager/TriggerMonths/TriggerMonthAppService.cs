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
using DM.UBP.Domain.Service.TriggerManager.TriggerMonths;
using DM.UBP.Domain.Service.TriggerManager;
using DM.UBP.Application.Dto.TriggerManager.TriggerMonths;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.TriggerManager.TriggerMonths
{
    /// <summary>
    /// 按月计算的触发器的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_TriggerManager.Pages_TriggerManager_TriggerMonths)]
    public class TriggerMonthAppService : ITriggerMonthAppService
    {
        private readonly ITriggerMonthManager _TriggerMonthManager;
        public TriggerMonthAppService(
           ITriggerMonthManager triggermonthmanager
           )
        {
            _TriggerMonthManager = triggermonthmanager;
        }

        public async Task<PagedResultDto<TriggerMonthOutputDto>> GetTriggerMonths()
        {
            var entities = await _TriggerMonthManager.GetAllTriggerMonthsAsync();
            var listDto = entities.MapTo<List<TriggerMonthOutputDto>>();

            return new PagedResultDto<TriggerMonthOutputDto>(
            listDto.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<TriggerMonthOutputDto>> GetTriggerMonths(PagedAndSortedInputDto input)
        {
            var entities = await _TriggerMonthManager.GetAllTriggerMonthsAsync();
            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";
            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));
            var listDto = pageEntities.MapTo<List<TriggerMonthOutputDto>>();

            return new PagedResultDto<TriggerMonthOutputDto>(
            entities.Count,
            listDto
            );
        }
        public async Task<TriggerMonthOutputDto> GetTriggerMonthById(int id)
        {
            var entity = await _TriggerMonthManager.GetTriggerMonthByIdAsync(id);
            return entity.MapTo<TriggerMonthOutputDto>();
        }
        [AbpAuthorize(AppPermissions_TriggerManager.Pages_TriggerManager_TriggerMonths_Create)]
        public async Task<bool> CreateTriggerMonth(TriggerMonthInputDto input)
        {
            var entity = input.MapTo<TriggerMonth>();
            return await _TriggerMonthManager.CreateTriggerMonthAsync(entity);
        }
        [AbpAuthorize(AppPermissions_TriggerManager.Pages_TriggerManager_TriggerMonths_Edit)]
        public async Task<bool> UpdateTriggerMonth(TriggerMonthInputDto input)
        {
            var entity = await _TriggerMonthManager.GetTriggerMonthByIdAsync(input.Id);
            input.MapTo(entity);
            return await _TriggerMonthManager.UpdateTriggerMonthAsync(entity);
        }
        [AbpAuthorize(AppPermissions_TriggerManager.Pages_TriggerManager_TriggerMonths_Delete)]
        public async Task DeleteTriggerMonth(EntityDto input)
        {
            var entity = await _TriggerMonthManager.GetTriggerMonthByIdAsync(input.Id);
            await _TriggerMonthManager.DeleteTriggerMonthAsync(entity);
        }
    }
}
