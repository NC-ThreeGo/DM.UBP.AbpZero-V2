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
using DM.UBP.Domain.Entity.ReportManager;

namespace DM.UBP.Domain.Service.ReportManager.ReportDataSources
{
    /// <summary>
    /// 报表数据源的Domain.Service.Interface
    /// <summary>
    public interface IReportDataSourceManager : IDomainService
    {
        Task<List<ReportDataSource>> GetAllReportDataSourcesAsync();

        Task<ReportDataSource> GetReportDataSourceByIdAsync(long id);

        Task<bool> CreateReportDataSourceAsync(ReportDataSource reportdatasource);

        Task<bool> UpdateReportDataSourceAsync(ReportDataSource reportdatasource);

        Task DeleteReportDataSourceAsync(ReportDataSource reportdatasource);

    }
}
