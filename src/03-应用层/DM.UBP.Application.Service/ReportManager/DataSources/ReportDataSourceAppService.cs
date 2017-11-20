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
using System;
using System.Xml;
using DM.UBP.Domain.Service.ReportManager.Templates;
using System.Data;
using DM.UBP.Domain.Service.ReportManager.Parameters;

namespace DM.UBP.Application.Service.ReportManager.DataSources
{
    /// <summary>
    /// 报表数据源的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_DataSources, AppPermissions_ReportManager.Pages_Reports)]
    public class ReportDataSourceAppService : IReportDataSourceAppService
    {
        private readonly IReportDataSourceManager _ReportDataSourceManager;
        private readonly IReportTemplateManager _ReportTemplateManager;
        private readonly IReportParameterManager _ReportParameterManager;
        public ReportDataSourceAppService(
           IReportDataSourceManager reportdatasourcemanager,
           IReportTemplateManager reporttemplatemanager,
           IReportParameterManager reportParameterManager
           )
        {
            _ReportTemplateManager = reporttemplatemanager;
            _ReportDataSourceManager = reportdatasourcemanager;
            _ReportParameterManager = reportParameterManager;
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
            var entity = input.MapTo<ReportDataSource>();
            try
            {
                SetReportColumns(input);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }

            return await _ReportDataSourceManager.CreateReportDataSourceAsync(entity);
        }
        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_DataSources_Edit)]
        public async Task<bool> UpdateReportDataSource(ReportDataSourceInputDto input)
        {
            var entity = await _ReportDataSourceManager.GetReportDataSourceByIdAsync(input.Id);
            input.MapTo(entity);

            try
            {
                SetReportColumns(input);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
            
            return await _ReportDataSourceManager.UpdateReportDataSourceAsync(entity);
        }
        /// <summary>
        /// 更新报表的数据列
        /// </summary>
        /// <param name="input"></param>
        private void SetReportColumns(ReportDataSourceInputDto input)
        {
            
            var template = _ReportTemplateManager.GetReportTemplateByIdAsync(input.Template_Id);

            XmlDocument xmlReport = new XmlDocument();
            xmlReport.Load(template.Result.FilePath);
            XmlNode nodeDictionary = xmlReport.SelectSingleNode("/Report/Dictionary");

            DelNodeDataSource(nodeDictionary, input.TableName);

            XmlNode nodeDataSource = xmlReport.CreateElement("TableDataSource");
            SetNodeAttribute(nodeDataSource, "Name", input.TableName);
            SetNodeAttribute(nodeDataSource, "ReferenceName", "ds_" + input.TableName + "." + "dt_" + input.TableName);
            SetNodeAttribute(nodeDataSource, "DataType", "System.Int32");
            SetNodeAttribute(nodeDataSource, "Enabled", "true");

            var columns = GetColumnsByDataSource(input);
            foreach (DataColumn column in columns)
            {
                XmlNode nodeColumn = xmlReport.CreateElement("Column");
                SetNodeAttribute(nodeColumn, "Name", column.ColumnName);
                SetNodeAttribute(nodeColumn, "DataType", column.DataType.FullName);

                nodeDataSource.AppendChild(nodeColumn);
            }

            nodeDictionary.AppendChild(nodeDataSource);

            xmlReport.Save(template.Result.FilePath);
        }

        /// <summary>
        /// 获取列
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private DataColumnCollection GetColumnsByDataSource(ReportDataSourceInputDto input)
        {
            string sql = input.CommandText;
            string conn = ConfigurationManager.ConnectionStrings[input.ConnkeyName].ConnectionString;

            //List<string> resultP = GetParamsBySql(sql);
            List<string> resultP = GetParamsByStr(input.DataParams);

            OracleParameter[] paras = new OracleParameter[resultP.Count];
            for (int i = 0; i < resultP.Count; i++)
            {
                paras[i] = new OracleParameter { ParameterName = resultP[i], Value = "" };
            }
            var table = OracleDbHelper.ExecuteDataset(conn,
                sql,
                input.CommandType == 1 ? System.Data.CommandType.Text : System.Data.CommandType.StoredProcedure,
                paras);

            return table.Tables[0].Columns;
        }

        /// <summary>
        /// 删除数据源节点
        /// </summary>
        /// <param name="nodeDictionary"></param>
        /// <param name="tableName"></param>
        private void DelNodeDataSource(XmlNode nodeDictionary, string tableName)
        {
            XmlNodeList nodeDataSources = nodeDictionary.SelectNodes("TableDataSource");
            foreach (XmlElement node in nodeDataSources)
            {
                if (node.Attributes["Name"].Value == tableName &&
                    node.Attributes["ReferenceName"].Value == "ds_" + tableName + "." + "dt_" + tableName)
                {
                    nodeDictionary.RemoveChild(node);
                    return;
                }
            }
        }

        private void SetNodeAttribute(XmlNode node, string AttName, string AttValue)
        {
            if (node.Attributes[AttName] != null)
            {
                node.Attributes[AttName].Value = AttValue;
                return;
            }
            XmlDocument xmlReport = new XmlDocument();
            XmlAttribute att = node.OwnerDocument.CreateAttribute(AttName);
            att.Value = AttValue;
            node.Attributes.Append(att);
        }

        /// <summary>
        /// 根据SQL提取参数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<string> GetParamsBySql(string sql)
        {
            List<string> resultP = new List<string>();
            Regex paramReg = new Regex(@"(?<!:)[^\w:]:(?!:)[\w:]+");
            MatchCollection matches = paramReg.Matches(sql);
            foreach (Match m in matches)
            {
                resultP.Add(m.Groups[0].Value.Substring(m.Groups[0].Value.IndexOf(":")));
            }
            return resultP;
        }
        /// <summary>
        /// 根据参数字符串提取参数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private List<string> GetParamsByStr(string str)
        {
            List<string> resilt = new List<string>();

            if (string.IsNullOrWhiteSpace(str))
                return resilt;

            string[] strArr = str.Split(',');
           
            foreach (var item in strArr)
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;
                resilt.Add(item);
            }
            return resilt;
        }

        [AbpAuthorize(AppPermissions_ReportManager.Pages_ReportManager_DataSources_Delete)]
        public async Task DeleteReportDataSource(EntityDto input)
        {
            var entity = await _ReportDataSourceManager.GetReportDataSourceByIdAsync(input.Id);

            try
            {
                var template = _ReportTemplateManager.GetReportTemplateByIdAsync(entity.Template_Id);

                XmlDocument xmlReport = new XmlDocument();
                xmlReport.Load(template.Result.FilePath);
                XmlNode nodeDictionary = xmlReport.SelectSingleNode("/Report/Dictionary");

                DelNodeDataSource(nodeDictionary, entity.TableName);

                xmlReport.Save(template.Result.FilePath);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }

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


        public async Task<List<DataSet>> GetDataSource(long template_Id, Dictionary<string, string> dicParameters)
        {
            var entityParameters = await _ReportParameterManager.GetAllReportParametersAsync();
            var parameters = entityParameters.Where(d => d.Template_Id == template_Id).OrderBy(d => d.Id);

            var entityDataSources = await _ReportDataSourceManager.GetAllReportDataSourcesAsync();
            var dataSources = entityDataSources.Where(d => d.Template_Id == template_Id).OrderBy(d => d.Id);

            List<DataSet> listDs = new List<DataSet>();
            foreach (var dataSource in dataSources)
            {
                string sql = dataSource.CommandText;
                string conn = ConfigurationManager.ConnectionStrings[dataSource.ConnkeyName].ConnectionString;

                //List<string> resultP = GetParamsBySql(sql);
                List<string> resultP = GetParamsByStr(dataSource.DataParams); 

                OracleParameter[] paras = new OracleParameter[resultP.Count];
                for (int i = 0; i < resultP.Count; i++)
                {
                    var dataParam = parameters.Where(p => p.ParameterName.ToUpper() == resultP[i].ToUpper());
                    if (dataParam.Count() == 0)
                        continue;

                    #region 判断类型
                    switch (dataParam.First().ParamterType)
                    {
                        case 1:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = resultP[i],
                                Value = dicParameters[resultP[i]]
                            };
                            break;
                        case 2:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = resultP[i],
                                Value = Convert.ToInt32(dicParameters[resultP[i]])
                            };
                            break;
                        case 3:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = resultP[i],
                                Value = Convert.ToDecimal(dicParameters[resultP[i]])
                            };
                            break;
                        case 4:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = resultP[i],
                                Value = Convert.ToDateTime(dicParameters[resultP[i]])
                            };
                            break;
                        case 5:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = resultP[i],
                                Value = Convert.ToBoolean(dicParameters[resultP[i]])
                            };
                            break;
                        case 6:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = resultP[i],
                                Value = new Guid(dicParameters[resultP[i]])
                            };
                            break;
                        default:
                            paras[i] = new OracleParameter
                            {
                                ParameterName = resultP[i],
                                Value = dicParameters[resultP[i]]
                            };
                            break;
                    }
                    #endregion

                }
                var dataSet = OracleDbHelper.ExecuteDataset(conn,
                    sql,
                    dataSource.CommandType == 1 ? System.Data.CommandType.Text : System.Data.CommandType.StoredProcedure,
                    paras);
                dataSet.Tables[0].TableName = "dt_" + dataSource.TableName;
                dataSet.DataSetName = "ds_" + dataSource.TableName;
                listDs.Add(dataSet);
            }
            return await Task.FromResult(listDs);
        }
    }
}
