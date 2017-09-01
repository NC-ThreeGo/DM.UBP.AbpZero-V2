using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using DM.UBP.Web.Areas.Mpa.Models.Common.Modals;
using DM.UBP.Web.Controllers;

namespace DM.UBP.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class CommonController : UBPControllerBase
    {
        public PartialViewResult LookupModal(LookupModalViewModel model)
        {
            return PartialView("Modals/_LookupModal", model);
        }
    }
}