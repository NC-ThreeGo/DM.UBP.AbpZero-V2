using System.Web.Mvc;
using Abp.Auditing;
using Abp.Web.Mvc.Authorization;

namespace DM.UBP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ApplicationController : UBPControllerBase
    {
        [DisableAuditing]
        public ActionResult Index()
        {
            /* Enable next line to redirect to Multi-Page Application */
            //默认启用MPA方式，如果注释掉则默认启用SPA
            return RedirectToAction("Index", "Home", new { area = "Mpa" });

            return View("~/App/common/views/layout/layout.cshtml"); //Layout of the angular application.
        }
    }
}