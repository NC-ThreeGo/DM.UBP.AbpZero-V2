using DM.UBP.Web.Controllers;
using System.Web.Mvc;

namespace DM.UBP.Web.Areas.ReportManage.Controllers
{
    public class DesignerController : UBPControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}