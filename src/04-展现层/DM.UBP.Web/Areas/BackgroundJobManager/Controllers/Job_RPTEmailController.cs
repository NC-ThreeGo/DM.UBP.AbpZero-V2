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
using DM.UBP.Application.Dto.BackgroundJobManager.Job_RPTEmails;
using DM.UBP.Application.Service.BackgroundJobManager.Job_RPTEmails;
using DM.UBP.Domain.Service.BackgroundJobManager;
using DM.UBP.Web.Controllers;

namespace DM.UBP.Web.Areas.BackgroundJobManager.Controllers
{
    /// <summary>
    /// 工作的Controller
    /// <summary>
    [AbpMvcAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_RPTEmails)]
    public class Job_RPTEmailController : UBPControllerBase
    {
        private IJob_RPTEmailAppService _Job_RPTEmailAppService;
        public Job_RPTEmailController(
           ICacheManager cacheManager,
           IJob_RPTEmailAppService job_rptemailappservice
           )
        {
            _Job_RPTEmailAppService = job_rptemailappservice;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_RPTEmails_Create)]
        public PartialViewResult CreateModal()
        {
            var viewModel = new Job_RPTEmailOutputDto()
            {
                //给属性赋值
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_RPTEmails_Edit)]
        public async Task<PartialViewResult> EditModal(long id)
        {
            var viewModel = await _Job_RPTEmailAppService.GetJob_RPTEmailById(id);
            return PartialView("_CreateOrEditModal", viewModel);
        }

    }
}
