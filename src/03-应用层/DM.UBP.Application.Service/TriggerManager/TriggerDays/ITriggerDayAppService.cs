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
using DM.UBP.Application.Dto.TriggerManager.TriggerDays;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.TriggerManager.TriggerDays
{
    /// <summary>
    /// 按天计算的触发器的Application.Service.Interface
    /// <summary>
    public interface ITriggerDayAppService : IApplicationService
    {
        Task<PagedResultDto<TriggerDayOutputDto>> GetTriggerDays();

        Task<PagedResultDto<TriggerDayOutputDto>> GetTriggerDays(PagedAndSortedInputDto input);

        Task<TriggerDayOutputDto> GetTriggerDayById(int id);

        Task<bool> CreateTriggerDay(TriggerDayInputDto input);

        Task<bool> UpdateTriggerDay(TriggerDayInputDto input);

        Task DeleteTriggerDay(EntityDto input);

    }
}
