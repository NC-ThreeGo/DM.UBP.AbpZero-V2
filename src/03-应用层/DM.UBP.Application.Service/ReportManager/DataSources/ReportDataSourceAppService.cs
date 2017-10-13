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
using DM.UBP.Domain.Service.ReportManager.DataSources;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Application.Dto.ReportManager.DataSources;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;
using System.Configuration;
using Abp.UI;
using DM.UBP.Common.DbHelper;
using DM.UBP.Application.Dto.ReportManager;
using Devart.Data.Oracle;
using System.Text.RegularExpressions;

namespace DM.UBP.Application.Service.ReportManager.DataSources
{
    /// <summary>
    /// 报表数据源的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_DataSources)]
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

            return new PagedResultDto<ReportDataSourceOutputDto>(
            listDto.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<ReportDataSourceOutputDto>> GetReportDataSources(PagedAndSortedInputDto input)
        {
            var entities = await _ReportDataSourceManager.GetAllReportDataSourcesAsync();
            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";
            var orderEntities = await Task.FromResult(entities.Where(d=>d.Template_Id==4).OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));
            var listDto = pageEntities.MapTo<List<ReportDataSourceOutputDto>>();

            return new PagedResultDto<ReportDataSourceOutputDto>(
            entities.Count,
            listDto
            );
        }

        public async Task<PagedResultDto<ReportDataSourceOutputDto>> GetReportDataSourcesByTemplate(EntityDto input)
        {
            var entities = await _ReportDataSourceManager.GetAllReportDataSourcesAsync();

            var dataSources = await Task.FromResult(entities.Where(d => d.Template_Id == input.Id).OrderBy(d => d.Id));

            var listDto = dataSources.MapTo<List<ReportDataSourceOutputDto>>();

            return new PagedResultDto<ReportDataSourceOutputDto>(
            listDto.Count,
            listDto
            );
        }

        public async Task<ReportDataSourceOutputDto> GetReportDataSourceById(long id)
        {
            var entity = await _ReportDataSourceManager.GetReportDataSourceByIdAsync(id);
            return entity.MapTo<ReportDataSourceOutputDto>();
        }
        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_DataSources_Create)]
        public async Task<bool> CreateReportDataSource(ReportDataSourceInputDto input)
        {
            //throw new UserFriendlyException("SQL错误");
            

            var entity = input.MapTo<ReportDataSource>();
            return await _ReportDataSourceManager.CreateReportDataSourceAsync(entity);
        }
        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_DataSources_Edit)]
        public async Task<bool> UpdateReportDataSource(ReportDataSourceInputDto input)
        {
            var entity = await _ReportDataSourceManager.GetReportDataSourceByIdAsync(input.Id);
            input.MapTo(entity);

            string sql = input.CommandText;

            string conn = ConfigurationManager.ConnectionStrings[input.ConnkeyName].ConnectionString;

            List<string> resultP = new List<string>();
            Regex paramReg = new Regex(@"(?<!:)[^\w:]:(?!:)[\w:]+");
            MatchCollection matches = paramReg.Matches(sql);
            foreach (Match m in matches)
            {
                resultP.Add(m.Groups[0].Value.Substring(m.Groups[0].Value.IndexOf(":")));
            }
            OracleParameter[] paras = new OracleParameter[resultP.Count];
            for (int i = 0; i < resultP.Count; i++)
            {
                paras[i] = new OracleParameter { ParameterName = resultP[i], Value = "" };
            }

            var table = OracleDbHelper.ExecuteDataset(conn,
                sql,
                input.CommandType == 1 ? System.Data.CommandType.Text : System.Data.CommandType.StoredProcedure,
                paras);


            return await _ReportDataSourceManager.UpdateReportDataSourceAsync(entity);
        }
        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_DataSources_Delete)]
        public async Task DeleteReportDataSource(EntityDto input)
        {
            var entity = await _ReportDataSourceManager.GetReportDataSourceByIdAsync(input.Id);
            await _ReportDataSourceManager.DeleteReportDataSourceAsync(entity);
        }

        public List<ComboboxItemDto> GetConnkeysToItem(string selectValue)
        {
            List<ComboboxItemDto> listItem = new List<ComboboxItemDto>();
            var connStrs = ConfigurationManager.ConnectionStrings;
            for (int i = 0; i < connStrs.Count; i++)
            {
                ComboboxItemDto comboxItem = new ComboboxItemDto(connStrs[i].Name, connStrs[i].Name) { IsSelected = connStrs[i].Name == selectValue };
                listItem.Add(comboxItem);
            }
            return listItem;
        }

        public List<ComboboxItemDto> GetCommandTypesToItem(int selectValue)
        {
            List<ComboboxItemDto> listItem = new List<ComboboxItemDto>();

            foreach (var item in ReportDefine.CommandTypes)
            {
                ComboboxItemDto comboxItem = new ComboboxItemDto(item.Value.ToString(), item.Key) { IsSelected = item.Value == selectValue };
                listItem.Add(comboxItem);
            }
            return listItem;
        }

    }
}
