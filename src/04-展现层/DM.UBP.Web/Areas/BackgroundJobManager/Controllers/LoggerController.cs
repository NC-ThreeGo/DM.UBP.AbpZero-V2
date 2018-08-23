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
using DM.UBP.Application.Dto.BackgroundJobManager.Loggers;
using DM.UBP.Application.Service.BackgroundJobManager.Loggers;
using DM.UBP.Domain.Service.BackgroundJobManager;
using DM.UBP.Web.Controllers;
using DM.UBP.Application.Service.BackgroundJobManager.JobGroups;

namespace DM.UBP.Web.Areas.BackgroundJobManager.Controllers
{
    /// <summary>
    /// 后台任务系统日志的Controller
    /// <summary>
    [AbpMvcAuthorize(AppPermissions_BackgroundJobManager.Pages_BackgroundJobManager_Loggers)]
    public class LoggerController : UBPControllerBase
    {
        private ILoggerAppService _LoggerAppService;
        private IJobGroupAppService _JobGroupAppService;

        public LoggerController(
           ICacheManager cacheManager,
           ILoggerAppService loggerappservice,
           IJobGroupAppService jobgroupappservice
           )
        {
            _LoggerAppService = loggerappservice;
            _JobGroupAppService = jobgroupappservice;
        }

        public ActionResult Index()
        {
            var jobGroups = _JobGroupAppService.GetJobTypesToItem("").Result;
            ViewBag.JobGroups = jobGroups;

            return View();
        }


        public PartialViewResult CreateModal()
        {
            var viewModel = new LoggerOutputDto()
            {
                //给属性赋值
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }


        public async Task<PartialViewResult> EditModal(long id)
        {
            var viewModel = await _LoggerAppService.GetLoggerById(id);
            return PartialView("_CreateOrEditModal", viewModel);
        }

    }
}
