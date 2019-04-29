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
using DM.UBP.Application.Dto.WeiXinManager.WeiXinApps;
using DM.UBP.Application.Service.WeiXinManager.WeiXinApps;
using DM.UBP.Domain.Service.WeiXinManager;
using DM.UBP.Web.Controllers;

namespace DM.UBP.Web.Areas.WeiXinManager.Controllers
{
    /// <summary>
    /// 的Controller
    /// <summary>
    [AbpMvcAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinApps)]
    public class WeiXinAppController : UBPControllerBase
    {
        private IWeiXinAppAppService _WeiXinAppAppService;
        public WeiXinAppController(
           ICacheManager cacheManager,
           IWeiXinAppAppService weixinappappservice
           )
        {
            _WeiXinAppAppService = weixinappappservice;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinApps_Create)]
        public PartialViewResult CreateModal(string corpId)
        {
            

            var viewModel = new WeiXinAppOutputDto()
            {
                //给属性赋值
                CorpId = corpId
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinApps_Edit)]
        public async Task<PartialViewResult> EditModal(long id)
        {
            var viewModel = await _WeiXinAppAppService.GetWeiXinAppById(id);
            return PartialView("_CreateOrEditModal", viewModel);
        }

    }
}
