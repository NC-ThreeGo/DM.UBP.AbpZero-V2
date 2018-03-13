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
using DM.UBP.Domain.Entity.TriggerManager;

namespace DM.UBP.Domain.Service.TriggerManager.TriggerDays
{
    /// <summary>
    /// 按天计算的触发器的Domain.Service
    /// <summary>
    public class TriggerDayManager : DomainService, ITriggerDayManager
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IRepository<TriggerDay, int> _triggerdayRepository;

        public TriggerDayManager(
           IUnitOfWorkManager unitOfWorkManager,
           IRepository<TriggerDay, int> triggerdayRepository
           )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _triggerdayRepository = triggerdayRepository;
        }

        public async Task<List<TriggerDay>> GetAllTriggerDaysAsync()
        {
            var triggerdays = _triggerdayRepository.GetAll().OrderBy(p => p.Id);
            return await triggerdays.ToListAsync();
        }

        public async Task<TriggerDay> GetTriggerDayByIdAsync(int id)
        {
            return await _triggerdayRepository.GetAsync(id);
        }

        public async Task<bool> CreateTriggerDayAsync(TriggerDay triggerday)
        {
            var entity = await _triggerdayRepository.InsertAsync(triggerday);
            return entity != null;
        }

        public async Task<bool> UpdateTriggerDayAsync(TriggerDay triggerday)
        {
            var entity = await _triggerdayRepository.UpdateAsync(triggerday);
            return entity != null;
        }

        public async Task DeleteTriggerDayAsync(TriggerDay triggerday)
        {
            await _triggerdayRepository.DeleteAsync(triggerday);
        }

    }
}
