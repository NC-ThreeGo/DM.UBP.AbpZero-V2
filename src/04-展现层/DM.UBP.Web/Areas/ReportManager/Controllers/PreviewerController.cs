﻿using Abp.Runtime.Caching;
using Abp.Web.Mvc.Authorization;
using DM.Common.Extensions;
using DM.Common.Security;
using DM.UBP.Application.Dto.ReportManager;
using DM.UBP.Application.Dto.ReportManager.Categories;
using DM.UBP.Application.Dto.ReportManager.Templates;
using DM.UBP.Application.Service.ReportManager.Categories;
using DM.UBP.Application.Service.ReportManager.DataSources;
using DM.UBP.Application.Service.ReportManager.Parameters;
using DM.UBP.Application.Service.ReportManager.Templates;
using DM.UBP.Common.DbHelper;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Web.Controllers;
using DM.UBP.Web.Session;
using FastReport.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DM.UBP.Web.Areas.ReportManager.Controllers
{
    [AbpMvcAuthorize(AppPermissions_ReportManager.Pages_Reports)]
    public class PreviewerController : UBPControllerBase
    {
        private IPerRequestSessionCache _sessionCache;
        private IReportTemplateAppService _TemplateAppService;
        private IReportParameterAppService _ParameterAppService;
        private IReportDataSourceAppService _DataSourceAppService;
        private IReportCategoryAppService _CategoryAppService;
        public PreviewerController(
           ICacheManager cacheManager,
           IPerRequestSessionCache sessionCache,
           IReportTemplateAppService templateAppService,
           IReportParameterAppService parameterAppService,
           IReportDataSourceAppService dataSourceAppService,
           IReportCategoryAppService categoryAppService
           )
        {
            _sessionCache = sessionCache;
            _TemplateAppService = templateAppService;
            _ParameterAppService = parameterAppService;
            _DataSourceAppService = dataSourceAppService;
            _CategoryAppService = categoryAppService;
        }

        private WebReport _webReport = new WebReport();


        public ActionResult Index(long id)
        {
            var tempalte = _TemplateAppService.GetReportTemplateById(id);

            _webReport.Report.Load(System.AppDomain.CurrentDomain.BaseDirectory + tempalte.Result.FilePath);

            var dicParameter = Request["parameterValues"].ToObject<Dictionary<string, string>>();

            var listDs = _DataSourceAppService.GetDataSource(id, dicParameter).Result;

            foreach (var ds in listDs)
            {
                _webReport.Report.RegisterData(ds, ds.DataSetName);
            }

            _webReport.Width = Unit.Percentage(100);
            _webReport.Height = Unit.Percentage(100);

            #region 预览
            _webReport.CurrentTab.Name = tempalte.Result.TemplateName;
            _webReport.ToolbarIconsStyle = ToolbarIconsStyle.Black;
            _webReport.ShowToolbar = true;
            _webReport.SinglePage = true;
            _webReport.ShowTabCloseButton = true;
            _webReport.TabPosition = TabPosition.InsideToolbar;

            #endregion
            #region 导出
            //_webReport.Export();
            //FastReport.Cloud.StorageClient.Ftp.FtpStorageClient ftp = new FastReport.Cloud.StorageClient.Ftp.FtpStorageClient();
            //FastReport.Export.Pdf.PDFExport pdf = new FastReport.Export.Pdf.PDFExport();
            //ftp.Server = "10.50.239.68";
            //ftp.Username = "administrator";
            //ftp.Password = "111.aaa";
            //_webReport.Report.Prepare();
            //ftp.SaveReport(_webReport.Report, pdf);
            #endregion

            #region 设计
            //_webReport.DesignReport = true;
            //_webReport.DesignScriptCode = false;
            //_webReport.Debug = true;
            //_webReport.DesignerPath = "~/Areas/ReportManager/Views/WebReportDesigner/index.html";
            //_webReport.DesignerSaveCallBack = "~/Areas/ReportManager/Designer/SaveDesignedReport";
            //_webReport.ID = "DesignReport";
            //_webReport.XlsxPageBreaks = false;
            //_webReport.XlsxSeamless = true;
            #endregion

            ViewBag.WebReport = _webReport;

            var viewModel = new ReportTemplateOutputDto()
            {
                //给属性赋值
            };

            return View(viewModel);
        }


        public ActionResult PreviewParameterModal(long id)
        {
            var parametes = _ParameterAppService.GetReportParametersByTemplate(new Abp.Application.Services.Dto.EntityDto { Id = Convert.ToInt32(id) }).Result;

            StringBuilder html = new StringBuilder();
            foreach (var item in parametes.Items)
            {
                if (item.UiType == (int)ReportDefine.UiTypes.日期型)
                {
                    html.AppendFormat("<div class=\"form-group form-md-line-input no-hint\">");
                    html.AppendFormat("<input type=\"text\" name=\"{0}\" class=\"form-control date-picker\" required>", item.ParameterName);
                    html.AppendFormat("<label>{0}</label>", item.LabelName);
                    html.AppendFormat("</div>");
                }
                else if (item.UiType == (int)ReportDefine.UiTypes.日期时间型)
                {
                    html.AppendFormat("<div class=\"form-group form-md-line-input no-hint\">");
                    html.AppendFormat("<input type=\"text\" name=\"{0}\" class=\"form-control date-time-picker\" required>", item.ParameterName);
                    html.AppendFormat("<label>{0}</label>", item.LabelName);
                    html.AppendFormat("</div>");
                }
                else if (item.UiType == (int)ReportDefine.UiTypes.下拉框
                    && !string.IsNullOrWhiteSpace(item.DynamicDataSource)
                    && !string.IsNullOrWhiteSpace(item.DynamicSql))
                {
                    string conn = ConfigurationManager.ConnectionStrings[item.DynamicDataSource].ConnectionString;

                    var table = OracleDbHelper.ExecuteDataset(conn, item.DynamicSql, System.Data.CommandType.Text).Tables[0];

                    html.AppendFormat("<div class=\"form-group\">");
                    html.AppendFormat("<label for=\"{0}\">{1}</label>", item.ParameterName, item.LabelName);
                    html.AppendFormat("<select id=\"{0}\" name=\"{1}\" class=\"form-control bs-select\" data-live-search=\"true\">", item.ParameterName, item.ParameterName);
                    foreach (DataRow row in table.Rows)
                    {
                        html.AppendFormat("<option data-icon=\"{0}\" value=\"{1}\")><i class=\"{2}\"></i>{3}</option>",
                            row["value"].ToString(),
                            row["value"].ToString(),
                            row["value"].ToString(),
                            row["text"].ToString());
                    }
                    html.AppendFormat("</select>");
                }
                else
                {
                    html.AppendFormat("<div class=\"form-group form-md-line-input no-hint\">");
                    html.AppendFormat("<input type=\"text\" name=\"{0}\" class=\"form-control\">", item.ParameterName);
                    html.AppendFormat("<label>{0}</label>", item.LabelName);
                    html.AppendFormat("</div>");
                }
            }
            ViewBag.FormHtml = html.ToString();
            ViewBag.Id = id;

            return PartialView("_PreviewParameterModal");
        }


        public ActionResult ReportList(long categoryId)
        {
            var model = _CategoryAppService.GetCategoryById(categoryId).Result;
            return View(model);
        }

        public ActionResult PBIReportList()
        {
            var loginInfo = _sessionCache.GetCurrentLoginInformationsAsync();
            ViewBag.UserName = loginInfo.Result.User.Name;
            return View();
        }
    }
}