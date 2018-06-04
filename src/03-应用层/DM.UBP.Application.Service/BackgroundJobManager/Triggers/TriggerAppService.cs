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
using DM.UBP.Domain.Service.BackgroundJobManager.Triggers;
using DM.UBP.Domain.Service.BackgroundJobManager;
using DM.UBP.Application.Dto.BackgroundJobManager.Triggers;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.BackgroundJobManager.Triggers
{
    /// <summary>
    /// 工作的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Triggers)]
    public class TriggerAppService : ITriggerAppService
    {
        private readonly ITriggerManager _TriggerManager;
        public TriggerAppService(
           ITriggerManager triggermanager
           )
        {
            _TriggerManager = triggermanager;
        }

        public async Task<PagedResultDto<TriggerOutputDto>> GetTriggers()
        {
            var entities = await _TriggerManager.GetAllTriggersAsync();
            var listDto = entities.MapTo<List<TriggerOutputDto>>();

            return new PagedResultDto<TriggerOutputDto>(
            listDto.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<TriggerOutputDto>> GetTriggers(PagedAndSortedInputDto input)
        {
            var entities = await _TriggerManager.GetAllTriggersAsync();
            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";
            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));
            var listDto = pageEntities.MapTo<List<TriggerOutputDto>>();

            return new PagedResultDto<TriggerOutputDto>(
            entities.Count,
            listDto
            );
        }
        public async Task<TriggerOutputDto> GetTriggerById(long id)
        {
            var entity = await _TriggerManager.GetTriggerByIdAsync(id);
            return entity.MapTo<TriggerOutputDto>();
        }
        [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Triggers_Create)]
        public async Task<bool> CreateTrigger(TriggerInputDto input)
        {
            var entity = input.MapTo<Trigger>();
            return await _TriggerManager.CreateTriggerAsync(entity);
        }
        [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Triggers_Edit)]
        public async Task<bool> UpdateTrigger(TriggerInputDto input)
        {
            var entity = await _TriggerManager.GetTriggerByIdAsync(input.Id);
            input.MapTo(entity);
            return await _TriggerManager.UpdateTriggerAsync(entity);
        }
        [AbpAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Triggers_Delete)]
        public async Task DeleteTrigger(EntityDto input)
        {
            var entity = await _TriggerManager.GetTriggerByIdAsync(input.Id);
            await _TriggerManager.DeleteTriggerAsync(entity);
        }

        public async Task<List<ComboboxItemDto>> GetTriggersToItem(long selectValue)
        {
            var entities = await _TriggerManager.GetAllTriggersAsync();
            var items = entities.Select(c => new ComboboxItemDto(c.Id.ToString(), c.TriggerName) { IsSelected = c.Id == selectValue }).ToList();
            return items;
        }
    }
}
