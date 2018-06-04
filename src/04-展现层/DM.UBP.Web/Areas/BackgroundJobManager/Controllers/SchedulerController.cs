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
using DM.UBP.Application.Dto.BackgroundJobManager.Schedulers;
using DM.UBP.Application.Service.BackgroundJobManager.Schedulers;
using DM.UBP.Domain.Service.BackgroundJobManager;
using DM.UBP.Web.Controllers;
using DM.UBP.Application.Service.BackgroundJobManager.JobGroups;
using DM.UBP.Application.Service.BackgroundJobManager.Triggers;
using DM.UBP.Application.Service.BackgroundJobManager.Job_RPTEmails;
using System;

namespace DM.UBP.Web.Areas.BackgroundJobManager.Controllers
{
    /// <summary>
    /// 工作的Controller
    /// <summary>
    [AbpMvcAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Schedulers)]
    public class SchedulerController : UBPControllerBase
    {
        private ISchedulerAppService _SchedulerAppService;
        private IJobGroupAppService _JobGroupAppService;
        private IJob_RPTEmailAppService _Job_RPTEmailAppService;
        private ITriggerAppService _TriggerAppService;

        public SchedulerController(
           ICacheManager cacheManager,
           ISchedulerAppService schedulerappservice,
           IJobGroupAppService jobgroupappservice,
           IJob_RPTEmailAppService job_rptemailappservice,
           ITriggerAppService triggerappservice
           )
        {
            _SchedulerAppService = schedulerappservice;
            _JobGroupAppService = jobgroupappservice;
            _Job_RPTEmailAppService = job_rptemailappservice;
            _TriggerAppService = triggerappservice;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Schedulers_Create)]
        public async Task<PartialViewResult> CreateModal()
        {
            var viewModel = new SchedulerOutputDto()
            {
                //给属性赋值
            };

            var jobGroups = await _JobGroupAppService.GetJobGroupsToItem(0);
            ViewBag.JobGroups = jobGroups;

            var triggers = await _TriggerAppService.GetTriggersToItem(0);
            ViewBag.Triggers = triggers;
            
            var jobGroupEntity = await _JobGroupAppService.GetJobGroupById(Convert.ToInt64(jobGroups[0].Value));
            if (jobGroupEntity.TypeTable == "BGJM_JOB_RPTEMAIL")
            {
                ViewBag.Jobs = await _Job_RPTEmailAppService.GetJobRPTEmailsToItem(0);
            }

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Schedulers_Edit)]
        public async Task<PartialViewResult> EditModal(long id)
        {
            var viewModel = await _SchedulerAppService.GetSchedulerById(id);

            var jobGroups = await _JobGroupAppService.GetJobGroupsToItem(viewModel.JobGroup_Id);
            ViewBag.JobGroups = jobGroups;

            var triggers = await _TriggerAppService.GetTriggersToItem(viewModel.Trigger_Id);
            ViewBag.Triggers = triggers;

            var jobGroupEntity = await _JobGroupAppService.GetJobGroupById(viewModel.JobGroup_Id);
            if (jobGroupEntity.TypeTable == "BGJM_JOB_RPTEMAIL")
            {
                ViewBag.Jobs = await _Job_RPTEmailAppService.GetJobRPTEmailsToItem(viewModel.Job_Id);
            }

            return PartialView("_CreateOrEditModal", viewModel);
        }

    }
}
