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
using DM.UBP.Domain.Entity.ReportManager;

namespace DM.UBP.Domain.Service.ReportManager.Parameters
{
    /// <summary>
    /// 报表参数的Domain.Service
    /// <summary>
    public class ReportParameterManager : DomainService, IReportParameterManager
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IRepository<ReportParameter, long> _reportparameterRepository;

        public ReportParameterManager(
           IUnitOfWorkManager unitOfWorkManager,
           IRepository<ReportParameter, long> reportparameterRepository
           )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _reportparameterRepository = reportparameterRepository;
        }

        public async Task<List<ReportParameter>> GetAllReportParametersAsync()
        {
            var reportparameters = _reportparameterRepository.GetAll().OrderBy(p => p.Id);
            return await reportparameters.ToListAsync();
        }

        public async Task<ReportParameter> GetReportParameterByIdAsync(long id)
        {
            return await _reportparameterRepository.GetAsync(id);
        }

        public async Task<bool> CreateReportParameterAsync(ReportParameter reportparameter)
        {
            var entity = await _reportparameterRepository.InsertAsync(reportparameter);
            return entity != null;
        }

        public async Task<bool> UpdateReportParameterAsync(ReportParameter reportparameter)
        {
            var entity = await _reportparameterRepository.UpdateAsync(reportparameter);
            return entity != null;
        }

        public async Task DeleteReportParameterAsync(ReportParameter reportparameter)
        {
            await _reportparameterRepository.DeleteAsync(reportparameter);
        }

    }
}
