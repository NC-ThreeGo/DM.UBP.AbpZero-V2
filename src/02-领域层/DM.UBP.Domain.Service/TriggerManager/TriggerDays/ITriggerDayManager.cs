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

namespace DM.UBP.Domain.Service.TriggerManager.TriggerDays
{
    /// <summary>
    /// 按天计算的触发器的Domain.Service.Interface
    /// <summary>
    public interface ITriggerDayManager : IDomainService
    {
        Task<List<TriggerDay>> GetAllTriggerDaysAsync();

        Task<TriggerDay> GetTriggerDayByIdAsync(int id);

        Task<bool> CreateTriggerDayAsync(TriggerDay triggerday);

        Task<bool> UpdateTriggerDayAsync(TriggerDay triggerday);

        Task DeleteTriggerDayAsync(TriggerDay triggerday);

    }
}
