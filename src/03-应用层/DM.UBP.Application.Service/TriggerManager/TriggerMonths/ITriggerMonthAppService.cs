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
using DM.UBP.Application.Dto.TriggerManager.TriggerMonths;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.TriggerManager.TriggerMonths
{
    /// <summary>
    /// 按月计算的触发器的Application.Service.Interface
    /// <summary>
    public interface ITriggerMonthAppService : IApplicationService
    {
        Task<PagedResultDto<TriggerMonthOutputDto>> GetTriggerMonths();

        Task<PagedResultDto<TriggerMonthOutputDto>> GetTriggerMonths(PagedAndSortedInputDto input);

        Task<TriggerMonthOutputDto> GetTriggerMonthById(int id);

        Task<bool> CreateTriggerMonth(TriggerMonthInputDto input);

        Task<bool> UpdateTriggerMonth(TriggerMonthInputDto input);

        Task DeleteTriggerMonth(EntityDto input);

    }
}
