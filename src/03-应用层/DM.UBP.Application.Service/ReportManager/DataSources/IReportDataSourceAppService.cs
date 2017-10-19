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
using DM.UBP.Application.Dto.ReportManager.DataSources;
using DM.UBP.Dto;
using System.Data;

namespace DM.UBP.Application.Service.ReportManager.DataSources
{
    /// <summary>
    /// 报表数据源的Application.Service.Interface
    /// <summary>
    public interface IReportDataSourceAppService : IApplicationService
    {
        Task<PagedResultDto<ReportDataSourceOutputDto>> GetReportDataSources();

        Task<PagedResultDto<ReportDataSourceOutputDto>> GetReportDataSources(PagedAndSortedInputDto input);

        Task<ReportDataSourceOutputDto> GetReportDataSourceById(long id);

        Task<bool> CreateReportDataSource(ReportDataSourceInputDto input);

        Task<bool> UpdateReportDataSource(ReportDataSourceInputDto input);

        Task DeleteReportDataSource(EntityDto input);

        Task<PagedResultDto<ReportDataSourceOutputDto>> GetReportDataSourcesByTemplate(EntityDto input);

        List<ComboboxItemDto> GetConnkeysToItem(string selectValue);

        List<ComboboxItemDto> GetCommandTypesToItem(int selectValue);

        Task<List<DataSet>> GetDataSource(long template_Id, Dictionary<string, string> dicParameters);
    }
}
