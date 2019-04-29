using System.Web.Mvc;

namespace DM.UBP.Web.Areas.WeiXinManager
{
    public class WeiXinManagerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WeiXinManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WeiXinManager_default",
                "WeiXinManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}