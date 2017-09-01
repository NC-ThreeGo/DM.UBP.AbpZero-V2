using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using DM.UBP.Authorization;
using DM.UBP.Web.Controllers;

namespace DM.UBP.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Tenant_Dashboard)]
    public class DashboardController : UBPControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}