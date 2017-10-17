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
using DM.UBP.Domain.Service.ReportManager.Parameters;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Application.Dto.ReportManager.Parameters;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;
using DM.UBP.Application.Service.ReportManager.Parameters;
using DM.UBP.Application.Dto.ReportManager;

namespace DM.UBP.Application.Service.ReportManager.Parameters
{
    /// <summary>
    /// 报表参数的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Parameters)]
    public class ReportParameterAppService : IReportParameterAppService
    {
        private readonly IReportParameterManager _ReportParameterManager;
        public ReportParameterAppService(
           IReportParameterManager reportparametermanager
           )
        {
            _ReportParameterManager = reportparametermanager;
        }

        public async Task<PagedResultDto<ReportParameterOutputDto>> GetReportParameters()
        {
            var entities = await _ReportParameterManager.GetAllReportParametersAsync();
            var listDto = entities.MapTo<List<ReportParameterOutputDto>>();

            return new PagedResultDto<ReportParameterOutputDto>(
            listDto.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<ReportParameterOutputDto>> GetReportParameters(PagedAndSortedInputDto input)
        {
            var entities = await _ReportParameterManager.GetAllReportParametersAsync();
            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";
            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));
            var listDto = pageEntities.MapTo<List<ReportParameterOutputDto>>();

            return new PagedResultDto<ReportParameterOutputDto>(
            entities.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<ReportParameterOutputDto>> GetReportParametersByTemplate(EntityDto input)
        {
            var entities = await _ReportParameterManager.GetAllReportParametersAsync();

            var parameters = await Task.FromResult(entities.Where(d => d.Template_Id == input.Id).OrderBy(d => d.Id));

            var listDto = parameters.MapTo<List<ReportParameterOutputDto>>();

            return new PagedResultDto<ReportParameterOutputDto>(
            listDto.Count,
            listDto
            );
        }

        public async Task<ReportParameterOutputDto> GetReportParameterById(long id)
        {
            var entity = await _ReportParameterManager.GetReportParameterByIdAsync(id);
            return entity.MapTo<ReportParameterOutputDto>();
        }
        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Parameters_Create)]
        public async Task<bool> CreateReportParameter(ReportParameterInputDto input)
        {
            var entity = input.MapTo<ReportParameter>();
            return await _ReportParameterManager.CreateReportParameterAsync(entity);
        }
        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Parameters_Edit)]
        public async Task<bool> UpdateReportParameter(ReportParameterInputDto input)
        {
            var entity = await _ReportParameterManager.GetReportParameterByIdAsync(input.Id);
            input.MapTo(entity);
            return await _ReportParameterManager.UpdateReportParameterAsync(entity);
        }
        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Parameters_Delete)]
        public async Task DeleteReportParameter(EntityDto input)
        {
            var entity = await _ReportParameterManager.GetReportParameterByIdAsync(input.Id);
            await _ReportParameterManager.DeleteReportParameterAsync(entity);
        }

        public List<ComboboxItemDto> GetParamterTypesToItem(int selectValue)
        {
            List<ComboboxItemDto> listItem = new List<ComboboxItemDto>();

            foreach (var item in ReportDefine.ParamterTypes)
            {
                ComboboxItemDto comboxItem = new ComboboxItemDto(item.Value.ToString(), item.Key) { IsSelected = item.Value == selectValue };
                listItem.Add(comboxItem);
            }
            return listItem;
        }

        public List<ComboboxItemDto> GetUiTypesToItem(int selectValue)
        {
            List<ComboboxItemDto> listItem = new List<ComboboxItemDto>();

            foreach (var item in ReportDefine.UiTypes)
            {
                ComboboxItemDto comboxItem = new ComboboxItemDto(item.Value.ToString(), item.Key) { IsSelected = item.Value == selectValue };
                listItem.Add(comboxItem);
            }
            return listItem;
        }
    }
}
