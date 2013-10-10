using System.Web.Mvc;
using System.Web.Routing;

namespace EckeSnuff {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Ajax_Handler",
                "ajax/{action}/{ticks}",
                new { controller = "Ajax", action = "index", ticks = "" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}