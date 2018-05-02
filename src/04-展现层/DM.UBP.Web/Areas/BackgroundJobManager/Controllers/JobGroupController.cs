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
using DM.UBP.Application.Dto.BackgroundJobManager.JobGroups;
using DM.UBP.Application.Service.BackgroundJobManager.JobGroups;
using DM.UBP.Domain.Service.BackgroundJobManager;
using DM.UBP.Web.Controllers;

namespace DM.UBP.Web.Areas.BackgroundJobManager.Controllers
{
    /// <summary>
    /// 工作组的Controller
    /// <summary>
    [AbpMvcAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_JobGroups)]
    public class JobGroupController : UBPControllerBase
    {
        private IJobGroupAppService _JobGroupAppService;
        public JobGroupController(
           ICacheManager cacheManager,
           IJobGroupAppService jobgroupappservice
           )
        {
            _JobGroupAppService = jobgroupappservice;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_JobGroups_Create)]
        public PartialViewResult CreateModal()
        {
            var viewModel = new JobGroupOutputDto()
            {
                //给属性赋值
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_JobGroups_Edit)]
        public async Task<PartialViewResult> EditModal(long id)
        {
            var viewModel = await _JobGroupAppService.GetJobGroupById(id);
            return PartialView("_CreateOrEditModal", viewModel);
        }

    }
}
