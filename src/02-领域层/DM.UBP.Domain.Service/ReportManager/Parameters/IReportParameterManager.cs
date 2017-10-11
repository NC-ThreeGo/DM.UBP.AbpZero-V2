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

namespace DM.UBP.Domain.Service.ReportManager.Parameters
{
    /// <summary>
    /// 报表参数的Domain.Service.Interface
    /// <summary>
    public interface IReportParameterManager : IDomainService
    {
        Task<List<ReportParameter>> GetAllReportParametersAsync();

        Task<ReportParameter> GetReportParameterByIdAsync(long id);

        Task<bool> CreateReportParameterAsync(ReportParameter reportparameter);

        Task<bool> UpdateReportParameterAsync(ReportParameter reportparameter);

        Task DeleteReportParameterAsync(ReportParameter reportparameter);

    }
}
