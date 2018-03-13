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

namespace DM.UBP.Domain.Service.TriggerManager.TriggerMonths
{
    /// <summary>
    /// 按月计算的触发器的Domain.Service
    /// <summary>
    public class TriggerMonthManager : DomainService, ITriggerMonthManager
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IRepository<TriggerMonth, int> _triggermonthRepository;

        public TriggerMonthManager(
           IUnitOfWorkManager unitOfWorkManager,
           IRepository<TriggerMonth, int> triggermonthRepository
           )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _triggermonthRepository = triggermonthRepository;
        }

        public async Task<List<TriggerMonth>> GetAllTriggerMonthsAsync()
        {
            var triggermonths = _triggermonthRepository.GetAll().OrderBy(p => p.Id);
            return await triggermonths.ToListAsync();
        }

        public async Task<TriggerMonth> GetTriggerMonthByIdAsync(int id)
        {
            return await _triggermonthRepository.GetAsync(id);
        }

        public async Task<bool> CreateTriggerMonthAsync(TriggerMonth triggermonth)
        {
            var entity = await _triggermonthRepository.InsertAsync(triggermonth);
            return entity != null;
        }

        public async Task<bool> UpdateTriggerMonthAsync(TriggerMonth triggermonth)
        {
            var entity = await _triggermonthRepository.UpdateAsync(triggermonth);
            return entity != null;
        }

        public async Task DeleteTriggerMonthAsync(TriggerMonth triggermonth)
        {
            await _triggermonthRepository.DeleteAsync(triggermonth);
        }

    }
}
