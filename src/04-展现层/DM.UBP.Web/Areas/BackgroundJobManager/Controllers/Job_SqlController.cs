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
using DM.UBP.Domain.Service.BackgroundJobManager;
using DM.UBP.Web.Controllers;
using DM.UBP.Application.Service.BackgroundJobManager.Job_Sqls;
using DM.UBP.Application.Dto.BackgroundJobManager.Job_Sqls;
using DM.UBP.Application.Service.BackgroundJobManager.JobGroups;
using DM.UBP.Application.Service.ReportManager.DataSources;

namespace DM.UBP.Web.Areas.BackgroundJobManager.Controllers
{
    /// <summary>
    /// SQL任务的Controller
    /// <summary>
    [AbpMvcAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_Sql)]
    public class Job_SqlController : UBPControllerBase
    {
        private IJob_SqlAppService _Job_SqlAppService;
        private IJobGroupAppService _JobGroupAppService;
        private IReportDataSourceAppService _ReportDataSourceAppService;
        public Job_SqlController(
           ICacheManager cacheManager,
           IJob_SqlAppService job_sqlappservice,
           IJobGroupAppService jobgroupappservice,
           IReportDataSourceAppService reportdatasourceappservice
           )
        {
            _Job_SqlAppService = job_sqlappservice;
            _JobGroupAppService = jobgroupappservice;
            _ReportDataSourceAppService = reportdatasourceappservice;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_Sql_Create)]
        public async Task<PartialViewResult> CreateModal()
        {
            var viewModel = new Job_SqlOutputDto()
            {
                //给属性赋值
            };
            var jobGroups = await _JobGroupAppService.GetJobGroupsToItem(21);
            ViewBag.JobGroups = jobGroups;

            var connkeys = _ReportDataSourceAppService.GetConnkeysToItem(string.Empty);
            ViewBag.Connkeys = connkeys;

            var commandTypes = _ReportDataSourceAppService.GetCommandTypesToItem(0);
            ViewBag.CommandTypes = commandTypes;

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Job_Sql_Edit)]
        public async Task<PartialViewResult> EditModal(long id)
        {
            var viewModel = await _Job_SqlAppService.GetJob_SqlById(id);

            var jobGroups = await _JobGroupAppService.GetJobGroupsToItem(viewModel.BGJM_JobGroup_Id);
            ViewBag.JobGroups = jobGroups;

            var connkeys = _ReportDataSourceAppService.GetConnkeysToItem(viewModel.ConnkeyName);
            ViewBag.Connkeys = connkeys;

            var commandTypes = _ReportDataSourceAppService.GetCommandTypesToItem(viewModel.CommandType);
            ViewBag.CommandTypes = commandTypes;

            return PartialView("_CreateOrEditModal", viewModel);
        }

    }
}
