using System.Web.Mvc;
using Abp.Auditing;
using Abp.Web.Mvc.Authorization;
using DM.UBP.Authorization;
using DM.UBP.Web.Controllers;

namespace DM.UBP.Web.Areas.Mpa.Controllers
{
    [DisableAuditing]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_AuditLogs)]
    public class AuditLogsController : UBPControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}