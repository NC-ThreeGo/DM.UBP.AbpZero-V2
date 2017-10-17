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
using DM.UBP.Application.Dto.ReportManager.Parameters;
using DM.UBP.Application.Service.ReportManager.Parameters;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Web.Controllers;

namespace DM.UBP.Web.Areas.ReportManager.Controllers
{
    /// <summary>
    /// 报表参数的Controller
    /// <summary>
    [AbpMvcAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Parameters)]
    public class ParameterController : UBPControllerBase
    {
        private IReportParameterAppService _ReportParameterAppService;
        public ParameterController(
           ICacheManager cacheManager,
           IReportParameterAppService reportparameterappservice
           )
        {
            _ReportParameterAppService = reportparameterappservice;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Parameters_Create)]
        public PartialViewResult CreateModal(long template_Id)
        {
            var viewModel = new ReportParameterOutputDto()
            {
                //给属性赋值
                Template_Id = template_Id
            };

            var paramterTypes = _ReportParameterAppService.GetParamterTypesToItem(0);
            ViewBag.ParamterTypes = paramterTypes;

            var uiTypes = _ReportParameterAppService.GetUiTypesToItem(0);
            ViewBag.UiTypes = uiTypes;

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions_ReportManager.Pages_ReportManager_Parameters_Edit)]
        public async Task<PartialViewResult> EditModal(long id)
        {
            var viewModel = await _ReportParameterAppService.GetReportParameterById(id);

            var paramterTypes = _ReportParameterAppService.GetParamterTypesToItem(viewModel.ParamterType);
            ViewBag.ParamterTypes = paramterTypes;

            var uiTypes = _ReportParameterAppService.GetUiTypesToItem(viewModel.UiType);
            ViewBag.UiTypes = uiTypes;

            return PartialView("_CreateOrEditModal", viewModel);
        }

    }
}
