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
using DM.UBP.Application.Dto.WeiXinManager.WeiXinConfigs;
using DM.UBP.Application.Service.WeiXinManager.WeiXinConfigs;
using DM.UBP.Domain.Service.WeiXinManager;
using DM.UBP.Web.Controllers;

namespace DM.UBP.Web.Areas.WeiXinManager.Controllers
{
    /// <summary>
    /// 的Controller
    /// <summary>
    [AbpMvcAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinConfigs)]
    public class WeiXinConfigController : UBPControllerBase
    {
        private IWeiXinConfigAppService _WeiXinConfigAppService;
        public WeiXinConfigController(
           ICacheManager cacheManager,
           IWeiXinConfigAppService weixinconfigappservice
           )
        {
            _WeiXinConfigAppService = weixinconfigappservice;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinConfigs_Create)]
        public PartialViewResult CreateModal()
        {
            var viewModel = new WeiXinConfigOutputDto()
            {
                //给属性赋值
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinConfigs_Edit)]
        public async Task<PartialViewResult> EditModal(long id)
        {
            var viewModel = await _WeiXinConfigAppService.GetWeiXinConfigById(id);
            return PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// 同步通讯录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinConfigs_Edit)]
        public async Task<PartialViewResult> SynchroModal(long id)
        {
            var viewModel = await _WeiXinConfigAppService.GetWeiXinConfigById(id);
            return PartialView("_SynchroModal", viewModel);
        }
    }
}
