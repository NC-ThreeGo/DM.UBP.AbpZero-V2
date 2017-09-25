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
using DM.UBP.Domain.Service.ReportManager.Templates;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Application.Dto.ReportManager.Templates;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;

namespace DM.UBP.Application.Service.ReportManager.Templates
{
    /// <summary>
    /// 报表模板的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Templates)]
    public class ReportTemplateAppService : IReportTemplateAppService
    {
        private readonly IReportTemplateManager _ReportTemplateManager;
        public ReportTemplateAppService(
           IReportTemplateManager reporttemplatemanager
           )
        {
            _ReportTemplateManager = reporttemplatemanager;
        }

        public async Task<PagedResultDto<ReportTemplateOutputDto>> GetReportTemplates()
        {
            var entities = await _ReportTemplateManager.GetAllReportTemplatesAsync();
            var listDto = entities.MapTo<List<ReportTemplateOutputDto>>();

            return new PagedResultDto<ReportTemplateOutputDto>(
            listDto.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<ReportTemplateOutputDto>> GetReportTemplates(PagedAndSortedInputDto input)
        {
            var entities = await _ReportTemplateManager.GetAllReportTemplatesAsync();
            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";
            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));
            var listDto = pageEntities.MapTo<List<ReportTemplateOutputDto>>();

            return new PagedResultDto<ReportTemplateOutputDto>(
            entities.Count,
            listDto
            );
        }
        public async Task<ReportTemplateOutputDto> GetReportTemplateById(long id)
        {
            var entity = await _ReportTemplateManager.GetReportTemplateByIdAsync(id);
            return entity.MapTo<ReportTemplateOutputDto>();
        }
        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Templates_Create)]
        public async Task<bool> CreateReportTemplate(ReportTemplateInputDto input)
        {
            var entity = input.MapTo<ReportTemplate>();
            return await _ReportTemplateManager.CreateReportTemplateAsync(entity);
        }
        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Templates_Edit)]
        public async Task<bool> UpdateReportTemplate(ReportTemplateInputDto input)
        {
            var entity = await _ReportTemplateManager.GetReportTemplateByIdAsync(input.Id);
            input.MapTo(entity);
            return await _ReportTemplateManager.UpdateReportTemplateAsync(entity);
        }
        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Templates_Delete)]
        public async Task DeleteReportTemplate(EntityDto input)
        {
            var entity = await _ReportTemplateManager.GetReportTemplateByIdAsync(input.Id);
            await _ReportTemplateManager.DeleteReportTemplateAsync(entity);
        }
    }
}
