using Abp.Runtime.Caching;
using Abp.Web.Mvc.Authorization;
using DM.Common.Extensions;
using DM.UBP.Application.Dto.ReportManager.Categories;
using DM.UBP.Application.Dto.ReportManager.Templates;
using DM.UBP.Application.Service.ReportManager.Categories;
using DM.UBP.Application.Service.ReportManager.DataSources;
using DM.UBP.Application.Service.ReportManager.Parameters;
using DM.UBP.Application.Service.ReportManager.Templates;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Web.Controllers;
using FastReport.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DM.UBP.Web.Areas.ReportManager.Controllers
{
    [AbpMvcAuthorize(AppPermissions_ReportManager.Pages_Reports)]
    public class PreviewerController: UBPControllerBase
    {
        private IReportTemplateAppService _TemplateAppService;
        private IReportParameterAppService _ParameterAppService;
        private IReportDataSourceAppService _DataSourceAppService;
        private IReportCategoryAppService _CategoryAppService;
        public PreviewerController(
           ICacheManager cacheManager,
           IReportTemplateAppService templateAppService,
           IReportParameterAppService parameterAppService,
           IReportDataSourceAppService dataSourceAppService,
           IReportCategoryAppService categoryAppService
           )
        {

            _TemplateAppService = templateAppService;
            _ParameterAppService = parameterAppService;
            _DataSourceAppService = dataSourceAppService;
            _CategoryAppService = categoryAppService;
        }

        private WebReport _webReport = new WebReport();


        public ActionResult Index(long id)
        {
            var dicParameter = Request["parameterValues"].ToObject<Dictionary<string, string>>();

            var listDs = _DataSourceAppService.GetDataSource(id, dicParameter).Result;

            foreach (var ds in listDs)
            {
                _webReport.Report.RegisterData(ds, ds.DataSetName);
            }

            //DataSet ds = new DataSet();
            //ds.DataSetName = "BaseUsers";
            //DataTable dt = new DataTable();
            //dt.TableName = "BaseUsers";
            //dt.Columns.Add("Id");
            //dt.Columns.Add("Name");

            //for (int i = 0; i < 100; i++)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr["Id"] = i.ToString();
            //    dr["Name"] = "test" + i.ToString();
            //    dt.Rows.Add(dr);
            //}
            //ds.Tables.Add(dt);

            //TableDataSource datasource = _webReport.Report.GetDataSource("protable") as TableDataSource;

            

            //string dirPath = System.AppDomain.CurrentDomain.BaseDirectory + "TemplateFiles\\";
            //string report_path = dirPath + "\\getUsers.frx";

            var tempalte = _TemplateAppService.GetReportTemplateById(id);

            _webReport.Report.Load(tempalte.Result.FilePath);

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
                if (item.UiType == 5)//日期型
                {
                    html.AppendFormat("<div class=\"form-group form-md-line-input form-md-floating-label no-hint\">");
                    html.AppendFormat("<input type=\"text\" name=\"{0}\" class=\"form-control date-picker\">", item.ParameterName);
                    html.AppendFormat("<label>{0}</label>", item.LabelName);
                    html.AppendFormat("</div>");
                }
                else if (item.UiType == 6)//日期时间型
                {
                    html.AppendFormat("<div class=\"form-group form-md-line-input form-md-floating-label no-hint\">");
                    html.AppendFormat("<input type=\"text\" name=\"{0}\" class=\"form-control date-time-picker\">", item.ParameterName);
                    html.AppendFormat("<label>{0}</label>", item.LabelName);
                    html.AppendFormat("</div>");
                }
                else
                {
                    html.AppendFormat("<div class=\"form-group form-md-line-input form-md-floating-label no-hint\">");
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
    }
}