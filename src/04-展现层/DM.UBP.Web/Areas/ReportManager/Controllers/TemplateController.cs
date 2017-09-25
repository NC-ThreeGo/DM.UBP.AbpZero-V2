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
using DM.UBP.Application.Dto.ReportManager.Templates;
using DM.UBP.Application.Service.ReportManager.Templates;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Web.Controllers;

namespace DM.UBP.Web.Areas.ReportManager.Controllers
{
    /// <summary>
    /// 报表模板的Controller
    /// <summary>
    [AbpMvcAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Templates)]
    public class TemplateController : UBPControllerBase
    {
        private IReportTemplateAppService _ReportTemplateAppService;
        public TemplateController(
           ICacheManager cacheManager,
           IReportTemplateAppService reporttemplateappservice
           )
        {
            _ReportTemplateAppService = reporttemplateappservice;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Templates_Create)]
        public PartialViewResult CreateModal()
        {
            var viewModel = new ReportTemplateOutputDto()
            {
                //给属性赋值
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Templates_Edit)]
        public async Task<PartialViewResult> EditModal(long id)
        {
            var viewModel = await _ReportTemplateAppService.GetReportTemplateById(id);
            return PartialView("_CreateOrEditModal", viewModel);
        }

    }
}
