using System.Web.Mvc;
using System.Web.Routing;

namespace PL.ASP_NET_MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "OpenAccount", id = UrlParameter.Optional }
            );
        }
    }
}
