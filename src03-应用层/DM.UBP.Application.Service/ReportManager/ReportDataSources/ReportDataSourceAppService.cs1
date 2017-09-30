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
using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Authorization;
using Abp.Application.Services.Dto;
using DM.UBP.Domain.Entity.ReportManager;
using DM.UBP.Domain.Service.ReportManager.ReportDataSources;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Application.Dto.ReportManager.ReportDataSources;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.ReportManager.ReportDataSources
{
/// <summary>
/// 报表数据源的Application.Service
/// <summary>
[AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_AppPermissions_ReportManager)]
public class ReportDataSourceAppService : IReportDataSourceAppService
{
private readonly IReportDataSourceManager _ReportDataSourceManager; 
public ReportDataSourceAppService(
   IReportDataSourceManager reportdatasourcemanager
   )
{
_ReportDataSourceManager = reportdatasourcemanager; 
}

public async Task<PagedResultDto<ReportDataSourceOutputDto>> GetReportDataSources()
{
var entities = await _ReportDataSourceManager.GetAllReportDataSourcesAsync();
var listDto = entities.MapTo<List<ReportDataSourceOutputDto>>();

return new PagedResultDto<ReportDataSourceOutputDto> (
listDto.Count,
listDto
); 
}
public async Task<PagedResultDto<ReportDataSourceOutputDto>> GetReportDataSources(PagedAndSortedInputDto input)
{
var entities = await _ReportDataSourceManager.GetAllReportDataSourcesAsync();
if (string.IsNullOrEmpty(input.Sorting))
input.Sorting = "Id";
var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));
var listDto = pageEntities.MapTo<List<ReportDataSourceOutputDto>>();

return new PagedResultDto<ReportDataSourceOutputDto> (
entities.Count,
listDto
); 
}
public async Task<ReportDataSourceOutputDto> GetReportDataSourceById(long id)
{
var entity = await _ReportDataSourceManager.GetReportDataSourceByIdAsync(id);
return entity.MapTo<ReportDataSourceOutputDto>();
}
[AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_AppPermissions_ReportManager_Create)]
public async Task<bool> CreateReportDataSource(ReportDataSourceInputDto input)
{
var entity = input.MapTo<ReportDataSource>();
return await _ReportDataSourceManager.CreateReportDataSourceAsync(entity);
}
[AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_AppPermissions_ReportManager_Edit)]
public async Task<bool> UpdateReportDataSource(ReportDataSourceInputDto input)
{
var entity = await _ReportDataSourceManager.GetReportDataSourceByIdAsync(input.Id);
input.MapTo(entity);
return await _ReportDataSourceManager.UpdateReportDataSourceAsync(entity);
}
[AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_AppPermissions_ReportManager_Delete)]
public async Task DeleteReportDataSource(EntityDto input)
{
var entity = await _ReportDataSourceManager.GetReportDataSourceByIdAsync(input.Id);
await _ReportDataSourceManager.DeleteReportDataSourceAsync(entity);
}
}
}
