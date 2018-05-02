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

namespace DM.UBP.Domain.Service.BackgroundJobManager.Triggers
{
    /// <summary>
    /// 工作的Domain.Service
    /// <summary>
    public class TriggerManager : DomainService, ITriggerManager
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IRepository<Trigger, long> _triggerRepository;

        public TriggerManager(
           IUnitOfWorkManager unitOfWorkManager,
           IRepository<Trigger, long> triggerRepository
           )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _triggerRepository = triggerRepository;
        }

        public async Task<List<Trigger>> GetAllTriggersAsync()
        {
            var triggers = _triggerRepository.GetAll().OrderBy(p => p.Id);
            return await triggers.ToListAsync();
        }

        public async Task<Trigger> GetTriggerByIdAsync(long id)
        {
            return await _triggerRepository.GetAsync(id);
        }

        public async Task<bool> CreateTriggerAsync(Trigger trigger)
        {
            var entity = await _triggerRepository.InsertAsync(trigger);
            return entity != null;
        }

        public async Task<bool> UpdateTriggerAsync(Trigger trigger)
        {
            var entity = await _triggerRepository.UpdateAsync(trigger);
            return entity != null;
        }

        public async Task DeleteTriggerAsync(Trigger trigger)
        {
            await _triggerRepository.DeleteAsync(trigger);
        }

    }
}
