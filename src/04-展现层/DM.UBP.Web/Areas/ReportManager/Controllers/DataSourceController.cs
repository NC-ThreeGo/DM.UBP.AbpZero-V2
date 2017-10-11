//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Web.Models;
using Abp.Runtime.Caching;
using Abp.Web.Mvc.Authorization;
using DM.UBP.Application.Dto.ReportManager.DataSources;
using DM.UBP.Application.Service.ReportManager.DataSources;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Web.Controllers;

namespace DM.UBP.Web.Areas.ReportManager.Controllers
{
    /// <summary>
    /// 报表数据源的Controller
    /// <summary>
    [AbpMvcAuthorize(AppPermissions_ReportManager.Pages_ReportManager_DataSources)]
    public class DataSourceController : UBPControllerBase
    {
        private IReportDataSourceAppService _ReportDataSourceAppService;
        public DataSourceController(
           ICacheManager cacheManager,
           IReportDataSourceAppService reportdatasourceappservice
           )
        {
            _ReportDataSourceAppService = reportdatasourceappservice;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions_ReportManager.Pages_ReportManager_DataSources_Create)]
        public PartialViewResult CreateModal(long template_Id)
        {
            var viewModel = new ReportDataSourceOutputDto()
            {
                Template_Id = template_Id
            };
            var connkeys = _ReportDataSourceAppService.GetConnkeysToItem(string.Empty);
            ViewBag.Connkeys = connkeys;

            var commandTypes = _ReportDataSourceAppService.GetCommandTypesToItem(0);
            ViewBag.CommandTypes = commandTypes;

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions_ReportManager.Pages_ReportManager_DataSources_Edit)]
        public async Task<PartialViewResult> EditModal(long id)
        {
            var viewModel = await _ReportDataSourceAppService.GetReportDataSourceById(id);

            var connkeys = _ReportDataSourceAppService.GetConnkeysToItem(viewModel.ConnkeyName);
            ViewBag.Connkeys = connkeys;

            var commandTypes = _ReportDataSourceAppService.GetCommandTypesToItem(viewModel.CommandType);
            ViewBag.CommandTypes = commandTypes;

            return PartialView("_CreateOrEditModal", viewModel);
        }

    }
}
