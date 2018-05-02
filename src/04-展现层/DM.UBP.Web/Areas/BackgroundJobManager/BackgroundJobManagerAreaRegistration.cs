using System.Web.Mvc;

namespace DM.UBP.Web.Areas.BackgroundJobManager
{
    public class BackgroundJobManagerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BackgroundJobManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BackgroundJobManager_default",
                "BackgroundJobManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}