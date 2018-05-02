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
using DM.UBP.Domain.Entity.BackgroundJobManager;

namespace DM.UBP.Domain.Service.BackgroundJobManager.Triggers
{
    /// <summary>
    /// 工作的Domain.Service.Interface
    /// <summary>
    public interface ITriggerManager : IDomainService
    {
        Task<List<Trigger>> GetAllTriggersAsync();

        Task<Trigger> GetTriggerByIdAsync(long id);

        Task<bool> CreateTriggerAsync(Trigger trigger);

        Task<bool> UpdateTriggerAsync(Trigger trigger);

        Task DeleteTriggerAsync(Trigger trigger);

    }
}
