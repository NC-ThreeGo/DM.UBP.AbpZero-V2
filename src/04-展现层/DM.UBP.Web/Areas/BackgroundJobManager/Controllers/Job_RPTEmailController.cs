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
using DM.UBP.Application.Service.BackgroundJobManager.JobGroups;
using DM.UBP.Application.Service.ReportManager.Templates;
using DM.UBP.Application.Service.ReportManager.Categories;

namespace DM.UBP.Web.Areas.BackgroundJobManager.Controllers
{
    /// <summary>
    /// 工作的Controller
    /// <summary>
    [AbpMvcAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_RPTEmails)]
    public class Job_RPTEmailController : UBPControllerBase
    {
        private IJob_RPTEmailAppService _Job_RPTEmailAppService;
        private IJobGroupAppService _JobGroupAppService;
        private IReportTemplateAppService _ReportTemplateAppService;
        private IReportCategoryAppService _CategoryAppService;
        public Job_RPTEmailController(
           ICacheManager cacheManager,
           IJob_RPTEmailAppService job_rptemailappservice,
           IJobGroupAppService jobgroupappservice,
           IReportTemplateAppService reporttemplateappservice,
           IReportCategoryAppService categoryappservice
           )
        {
            _Job_RPTEmailAppService = job_rptemailappservice;
            _JobGroupAppService = jobgroupappservice;
            _ReportTemplateAppService = reporttemplateappservice;
            _CategoryAppService = categoryappservice;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_RPTEmails_Create)]
        public async Task<PartialViewResult> CreateModal()
        {
            var viewModel = new Job_RPTEmailOutputDto()
            {
                //给属性赋值
            };
            var jobGroups = await _JobGroupAppService.GetJobGroupsToItem(1);
            ViewBag.JobGroups = jobGroups;

            //var categories = await _CategoryAppService.GetCategoriesToItem(0);
            //ViewBag.Categories = categories;

            var reportTemplates = await _ReportTemplateAppService.GetReportTemplatesToItem(0);
            ViewBag.ReportTemplates = reportTemplates;

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_RPTEmails_Edit)]
        public async Task<PartialViewResult> EditModal(long id)
        {
            var viewModel = await _Job_RPTEmailAppService.GetJob_RPTEmailById(id);

            var jobGroups = await _JobGroupAppService.GetJobGroupsToItem(viewModel.BGJM_JobGroup_Id);
            ViewBag.JobGroups = jobGroups;

            //var categories = await _CategoryAppService.GetCategoriesToItem(0);
            //ViewBag.Categories = categories;

            var reportTemplates = await _ReportTemplateAppService.GetReportTemplatesToItem(viewModel.Template_Id);
            ViewBag.ReportTemplates = reportTemplates;

            return PartialView("_CreateOrEditModal", viewModel);
        }

    }
}
