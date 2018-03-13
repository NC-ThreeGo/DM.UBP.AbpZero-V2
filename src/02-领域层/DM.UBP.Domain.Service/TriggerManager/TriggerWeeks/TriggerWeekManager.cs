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

namespace DM.UBP.Domain.Service.TriggerManager.TriggerWeeks
{
    /// <summary>
    /// 按周计算的触发器的Domain.Service
    /// <summary>
    public class TriggerWeekManager : DomainService, ITriggerWeekManager
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IRepository<TriggerWeek, int> _triggerweekRepository;

        public TriggerWeekManager(
           IUnitOfWorkManager unitOfWorkManager,
           IRepository<TriggerWeek, int> triggerweekRepository
           )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _triggerweekRepository = triggerweekRepository;
        }

        public async Task<List<TriggerWeek>> GetAllTriggerWeeksAsync()
        {
            var triggerweeks = _triggerweekRepository.GetAll().OrderBy(p => p.Id);
            return await triggerweeks.ToListAsync();
        }

        public async Task<TriggerWeek> GetTriggerWeekByIdAsync(int id)
        {
            return await _triggerweekRepository.GetAsync(id);
        }

        public async Task<bool> CreateTriggerWeekAsync(TriggerWeek triggerweek)
        {
            var entity = await _triggerweekRepository.InsertAsync(triggerweek);
            return entity != null;
        }

        public async Task<bool> UpdateTriggerWeekAsync(TriggerWeek triggerweek)
        {
            var entity = await _triggerweekRepository.UpdateAsync(triggerweek);
            return entity != null;
        }

        public async Task DeleteTriggerWeekAsync(TriggerWeek triggerweek)
        {
            await _triggerweekRepository.DeleteAsync(triggerweek);
        }

    }
}
