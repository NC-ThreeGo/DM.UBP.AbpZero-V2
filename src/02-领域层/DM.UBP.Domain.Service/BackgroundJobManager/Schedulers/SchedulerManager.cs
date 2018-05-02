//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Domain.Uow;
using Abp.Domain.Services;
using Abp.Domain.Repositories;
using DM.UBP.Domain.Entity.BackgroundJobManager;

namespace DM.UBP.Domain.Service.BackgroundJobManager.Schedulers
{
    /// <summary>
    /// 工作的Domain.Service
    /// <summary>
    public class SchedulerManager : DomainService, ISchedulerManager
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IRepository<Scheduler, long> _schedulerRepository;

        public SchedulerManager(
           IUnitOfWorkManager unitOfWorkManager,
           IRepository<Scheduler, long> schedulerRepository
           )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _schedulerRepository = schedulerRepository;
        }

        public async Task<List<Scheduler>> GetAllSchedulersAsync()
        {
            var schedulers = _schedulerRepository.GetAll().OrderBy(p => p.Id);
            return await schedulers.ToListAsync();
        }

        public async Task<Scheduler> GetSchedulerByIdAsync(long id)
        {
            return await _schedulerRepository.GetAsync(id);
        }

        public async Task<bool> CreateSchedulerAsync(Scheduler scheduler)
        {
            var entity = await _schedulerRepository.InsertAsync(scheduler);
            return entity != null;
        }

        public async Task<bool> UpdateSchedulerAsync(Scheduler scheduler)
        {
            var entity = await _schedulerRepository.UpdateAsync(scheduler);
            return entity != null;
        }

        public async Task DeleteSchedulerAsync(Scheduler scheduler)
        {
            await _schedulerRepository.DeleteAsync(scheduler);
        }

    }
}
