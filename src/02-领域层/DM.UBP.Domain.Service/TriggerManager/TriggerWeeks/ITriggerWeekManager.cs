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
using Abp.Domain.Services;
using DM.UBP.Domain.Entity.TriggerManager;

namespace DM.UBP.Domain.Service.TriggerManager.TriggerWeeks
{
    /// <summary>
    /// 按周计算的触发器的Domain.Service.Interface
    /// <summary>
    public interface ITriggerWeekManager : IDomainService
    {
        Task<List<TriggerWeek>> GetAllTriggerWeeksAsync();

        Task<TriggerWeek> GetTriggerWeekByIdAsync(int id);

        Task<bool> CreateTriggerWeekAsync(TriggerWeek triggerweek);

        Task<bool> UpdateTriggerWeekAsync(TriggerWeek triggerweek);

        Task DeleteTriggerWeekAsync(TriggerWeek triggerweek);

    }
}
