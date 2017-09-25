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

namespace DM.UBP.Domain.Service.ReportManager.Templates
{
    /// <summary>
    /// 报表模板的Domain.Service.Interface
    /// <summary>
    public interface IReportTemplateManager : IDomainService
    {
        Task<List<ReportTemplate>> GetAllReportTemplatesAsync();

        Task<ReportTemplate> GetReportTemplateByIdAsync(long id);

        Task<bool> CreateReportTemplateAsync(ReportTemplate reporttemplate);

        Task<bool> UpdateReportTemplateAsync(ReportTemplate reporttemplate);

        Task DeleteReportTemplateAsync(ReportTemplate reporttemplate);

    }
}
