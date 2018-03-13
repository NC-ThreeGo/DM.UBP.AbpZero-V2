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

namespace DM.UBP.Domain.Service.TriggerManager.TriggerMonths
{
    /// <summary>
    /// 按月计算的触发器的Domain.Service.Interface
    /// <summary>
    public interface ITriggerMonthManager : IDomainService
    {
        Task<List<TriggerMonth>> GetAllTriggerMonthsAsync();

        Task<TriggerMonth> GetTriggerMonthByIdAsync(int id);

        Task<bool> CreateTriggerMonthAsync(TriggerMonth triggermonth);

        Task<bool> UpdateTriggerMonthAsync(TriggerMonth triggermonth);

        Task DeleteTriggerMonthAsync(TriggerMonth triggermonth);

    }
}
