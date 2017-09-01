using System.Web.Mvc;

namespace DM.UBP.Web.Controllers
{
    public class HomeController : UBPControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}