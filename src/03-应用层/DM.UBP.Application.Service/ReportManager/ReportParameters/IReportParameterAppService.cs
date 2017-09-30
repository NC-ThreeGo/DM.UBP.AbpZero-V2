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
using DM.UBP.Application.Dto.ReportManager.ReportParameters;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.ReportManager.ReportParameters
{
    /// <summary>
    /// 报表参数的Application.Service.Interface
    /// <summary>
    public interface IReportParameterAppService : IApplicationService
    {
        Task<PagedResultDto<ReportParameterOutputDto>> GetReportParameters();

        Task<PagedResultDto<ReportParameterOutputDto>> GetReportParameters(PagedAndSortedInputDto input);

        Task<ReportParameterOutputDto> GetReportParameterById(long id);

        Task<bool> CreateReportParameter(ReportParameterInputDto input);

        Task<bool> UpdateReportParameter(ReportParameterInputDto input);

        Task DeleteReportParameter(EntityDto input);

    }
}
