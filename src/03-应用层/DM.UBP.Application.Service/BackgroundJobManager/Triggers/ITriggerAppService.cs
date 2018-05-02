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
using DM.UBP.Application.Dto.BackgroundJobManager.Triggers;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.BackgroundJobManager.Triggers
{
    /// <summary>
    /// 工作的Application.Service.Interface
    /// <summary>
    public interface ITriggerAppService : IApplicationService
    {
        Task<PagedResultDto<TriggerOutputDto>> GetTriggers();

        Task<PagedResultDto<TriggerOutputDto>> GetTriggers(PagedAndSortedInputDto input);

        Task<TriggerOutputDto> GetTriggerById(long id);

        Task<bool> CreateTrigger(TriggerInputDto input);

        Task<bool> UpdateTrigger(TriggerInputDto input);

        Task DeleteTrigger(EntityDto input);

    }
}
