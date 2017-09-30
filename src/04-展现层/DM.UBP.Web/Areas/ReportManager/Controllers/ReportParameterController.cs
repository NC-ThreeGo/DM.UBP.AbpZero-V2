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
using DM.UBP.Application.Dto.ReportManager.ReportParameters;
using DM.UBP.Application.Service.ReportManager.ReportParameters;
using DM.UBP.Domain.Service.ReportManager;
using DM.UBP.Web.Controllers;

namespace DM.UBP.Web.Areas.ReportManager.Controllers
{
/// <summary>
/// 报表参数的Controller
/// <summary>
[AbpMvcAuthorize(AppPermissions_ReportManager.Pages_ReportManager_AppPermissions_ReportManager)]
public class ReportParameterController : UBPControllerBase
{
private IReportParameterAppService _ReportParameterAppService; 
public ReportParameterController(
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

[AbpMvcAuthorize(AppPermissions_ReportManager.Pages_ReportManager_AppPermissions_ReportManager_Create)]
public PartialViewResult CreateModal()
{
var viewModel = new ReportParameterOutputDto()
{
//给属性赋值
};

return PartialView("_CreateOrEditModal", viewModel);
}

[AbpMvcAuthorize(AppPermissions_ReportManager.Pages_ReportManager_AppPermissions_ReportManager_Edit)]
public async Task<PartialViewResult> EditModal(long id)
{
var viewModel = await _ReportParameterAppService.GetReportParameterById(id);
return PartialView("_CreateOrEditModal", viewModel);
}

}
}
