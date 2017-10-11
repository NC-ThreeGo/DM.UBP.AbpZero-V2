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

namespace DM.UBP.Domain.Service.ReportManager.DataSources
{
    /// <summary>
    /// 报表数据源的Domain.Service
    /// <summary>
    public class ReportDataSourceManager : DomainService, IReportDataSourceManager
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IRepository<ReportDataSource, long> _reportdatasourceRepository;

        public ReportDataSourceManager(
           IUnitOfWorkManager unitOfWorkManager,
           IRepository<ReportDataSource, long> reportdatasourceRepository
           )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _reportdatasourceRepository = reportdatasourceRepository;
        }

        public async Task<List<ReportDataSource>> GetAllReportDataSourcesAsync()
        {
            var reportdatasources = _reportdatasourceRepository.GetAll().OrderBy(p => p.Id);
            return await reportdatasources.ToListAsync();
        }

        public async Task<ReportDataSource> GetReportDataSourceByIdAsync(long id)
        {
            return await _reportdatasourceRepository.GetAsync(id);
        }

        public async Task<bool> CreateReportDataSourceAsync(ReportDataSource reportdatasource)
        {
            var entity = await _reportdatasourceRepository.InsertAsync(reportdatasource);
            return entity != null;
        }

        public async Task<bool> UpdateReportDataSourceAsync(ReportDataSource reportdatasource)
        {
            var entity = await _reportdatasourceRepository.UpdateAsync(reportdatasource);
            return entity != null;
        }

        public async Task DeleteReportDataSourceAsync(ReportDataSource reportdatasource)
        {
            await _reportdatasourceRepository.DeleteAsync(reportdatasource);
        }

    }
}
