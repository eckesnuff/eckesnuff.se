using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using EckeSnuff.Dropbox;
using EckeSnuff.Dropbox.Hosting;

namespace EckeSnuff {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("dropbox/{*pathInfo}");
            routes.MapRoute(
                "Ajax_Handler",
                "ajax/{action}/{ticks}",
                new {controller = "Ajax", action = "index", ticks = ""}
                );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                );
                System.Web.Hosting.HostingEnvironment.RegisterVirtualPathProvider(
                new DropboxVirtualPathProvider(new DropboxService(ConfigurationManager.AppSettings["DropboxAppKey"],
                                                                  ConfigurationManager.AppSettings["DropboxAppSecret"],
                                                                  ConfigurationManager.AppSettings["DropboxUserName"],
                                                                  ConfigurationManager.AppSettings["DropboxPassword"])));
        }

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}