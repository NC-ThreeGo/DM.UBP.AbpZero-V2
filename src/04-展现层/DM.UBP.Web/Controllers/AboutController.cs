using System.Web.Mvc;

namespace DM.UBP.Web.Controllers
{
    public class AboutController : UBPControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}