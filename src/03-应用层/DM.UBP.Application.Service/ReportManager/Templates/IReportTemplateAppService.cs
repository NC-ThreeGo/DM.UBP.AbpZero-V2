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
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DM.UBP.Application.Dto.ReportManager.Templates;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.ReportManager.Templates
{
    /// <summary>
    /// 报表模板的Application.Service.Interface
    /// <summary>
    public interface IReportTemplateAppService : IApplicationService
    {
        Task<PagedResultDto<ReportTemplateOutputDto>> GetReportTemplates();

        Task<PagedResultDto<ReportTemplateOutputDto>> GetReportTemplates(PagedAndSortedInputDto input);

        Task<PagedResultDto<ReportTemplateOutputDto>> GetReportList(ReportListInputDto input);

        Task<ReportTemplateOutputDto> GetReportTemplateById(long id);

        Task<bool> CreateReportTemplate(ReportTemplateInputDto input);

        Task<bool> UpdateReportTemplate(ReportTemplateInputDto input);

        Task DeleteReportTemplate(EntityDto input);

        

    }
}
