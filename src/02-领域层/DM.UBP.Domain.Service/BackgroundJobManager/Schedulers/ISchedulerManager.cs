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

namespace DM.UBP.Domain.Service.BackgroundJobManager.Schedulers
{
    /// <summary>
    /// 工作的Domain.Service.Interface
    /// <summary>
    public interface ISchedulerManager : IDomainService
    {
        Task<List<Scheduler>> GetAllSchedulersAsync();

        Task<Scheduler> GetSchedulerByIdAsync(long id);

        Task<bool> CreateSchedulerAsync(Scheduler scheduler);

        Task<bool> UpdateSchedulerAsync(Scheduler scheduler);

        Task DeleteSchedulerAsync(Scheduler scheduler);
    }
}
