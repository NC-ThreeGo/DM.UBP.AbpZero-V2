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
using Abp.Application.Services.Dto;
using DM.UBP.Application.Service.ReportManager.Categories;

namespace DM.UBP.Web.Areas.ReportManager.Controllers
{
    /// <summary>
    /// 报表模板的Controller
    /// <summary>
    [AbpMvcAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Templates)]
    public class TemplateController : UBPControllerBase
    {
        private IReportTemplateAppService _ReportTemplateAppService;
        private IReportCategoryAppService _CategoryAppService;
        public TemplateController(
           ICacheManager cacheManager,
           IReportTemplateAppService reporttemplateappservice,
            IReportCategoryAppService categoryappservice
           )
        {
            _ReportTemplateAppService = reporttemplateappservice;
            _CategoryAppService = categoryappservice;
        }

        public ActionResult Index()
        {
            var categories = _CategoryAppService.GetCategoriesToItem(0);
            ViewBag.Categories = categories.Result;
            return View();
        }

        [AbpMvcAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Templates_Create)]
        public async Task<PartialViewResult> CreateModal()
        {
            var viewModel = new ReportTemplateOutputDto()
            {
                //给属性赋值
            };
            var categories = await _CategoryAppService.GetCategoriesToItem(0);
            ViewBag.Categories = categories;
            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Templates_Edit)]
        public async Task<PartialViewResult> EditModal(long id)
        {
            var viewModel = await _ReportTemplateAppService.GetReportTemplateById(id);
            var categories = await _CategoryAppService.GetCategoriesToItem(viewModel.Category_Id);
            ViewBag.Categories = categories;

            return PartialView("_CreateOrEditModal", viewModel);
        }

    }
}
