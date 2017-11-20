using Abp.Runtime.Caching;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using DM.UBP.Application.Service.ReportManager.Categories;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Web.Controllers;
using FastReport.Web;
using DM.UBP.Authorization.Users;
using System.Data;
using System;
using System.IO;
using FastReport.Data;
using DM.UBP.Application.Service.ReportManager.Templates;
using DM.UBP.Application.Dto.ReportManager.Templates;
using System.Web.UI.WebControls;

namespace DM.UBP.Web.Areas.ReportManager.Controllers
{
    [AbpMvcAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Designer)]
    public class DesignerController : UBPControllerBase
    {
        private IReportTemplateAppService _TemplateAppService;
        public DesignerController(
           ICacheManager cacheManager,
           IReportTemplateAppService templateAppService
           )
        {

            _TemplateAppService = templateAppService;
        }

        private WebReport _webReport = new WebReport();

        public ActionResult Index(long id)
        {
            //string report_path = Request.PhysicalApplicationPath;

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

            //_webReport.Report.RegisterData(ds, "BaseUsers");

            var tempalte = _TemplateAppService.GetReportTemplateById(id);

            _webReport.Report.Load(System.AppDomain.CurrentDomain.BaseDirectory + tempalte.Result.FilePath);

            _webReport.Width = Unit.Percentage(100);
            _webReport.Height = Unit.Percentage(100);

            #region 预览
            //_webReport.ToolbarIconsStyle = ToolbarIconsStyle.Black;
            //_webReport.ShowToolbar = false;
            //_webReport.SinglePage = true;
            #endregion

            #region 设计
            
            _webReport.DesignReport = true;
            _webReport.DesignScriptCode = true;
            _webReport.Debug = true;
            //_webReport.DesignerPath = "~/Areas/ReportManager/Views/WebReportDesigner/index.html";
            _webReport.DesignerPath = "~/WebReportDesigner/index.html";
            _webReport.DesignerSaveCallBack = "~/ReportManager/Designer/SaveDesignedReport";
            _webReport.DesignerSavePath = "~/App_Data/DesignedReports";
            //_webReport.DesignerLocale = "cn";
            _webReport.ID = id.ToString();
            _webReport.XlsxPageBreaks = false;
            _webReport.XlsxSeamless = true;
            #endregion
            
            ViewBag.WebReport = _webReport;

            var viewModel = new ReportTemplateOutputDto()
            {
                //给属性赋值
            };

            return View(viewModel);
        }

       

        //[HttpPost]
        public ActionResult SaveDesignedReport(string reportID, string reportUUID)
        {
            ViewBag.Message = String.Format("Confirmed {0} {1}", reportID, reportUUID);

            //Stream reportForSave = Request.InputStream;
            //string pathToSave = Server.MapPath("/TemplateFiles/getUsers.frx");
            //using (FileStream file = new FileStream(pathToSave, FileMode.Create))
            //{
            //    reportForSave.CopyTo(file);
            //}

            var tempalte = _TemplateAppService.GetReportTemplateById(int.Parse(reportID));
            var tempFile = Server.MapPath("~/App_Data/DesignedReports/") + reportUUID;

            _webReport.Report.Load(tempFile);
            _webReport.Report.Save(System.AppDomain.CurrentDomain.BaseDirectory + tempalte.Result.FilePath);

            FileInfo file = new FileInfo(tempFile);
            if (file.Exists)
                file.Delete();


            return PartialView("SaveDesignedReport");
        }
    }
}