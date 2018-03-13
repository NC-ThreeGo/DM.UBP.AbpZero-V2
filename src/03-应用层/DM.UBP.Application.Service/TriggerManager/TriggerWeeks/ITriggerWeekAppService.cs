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
using DM.UBP.Application.Dto.TriggerManager.TriggerWeeks;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.TriggerManager.TriggerWeeks
{
    /// <summary>
    /// 按周计算的触发器的Application.Service.Interface
    /// <summary>
    public interface ITriggerWeekAppService : IApplicationService
    {
        Task<PagedResultDto<TriggerWeekOutputDto>> GetTriggerWeeks();

        Task<PagedResultDto<TriggerWeekOutputDto>> GetTriggerWeeks(PagedAndSortedInputDto input);

        Task<TriggerWeekOutputDto> GetTriggerWeekById(int id);

        Task<bool> CreateTriggerWeek(TriggerWeekInputDto input);

        Task<bool> UpdateTriggerWeek(TriggerWeekInputDto input);

        Task DeleteTriggerWeek(EntityDto input);

    }
}
