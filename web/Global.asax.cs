using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Stormbreaker.Dashboard;
using Stormbreaker.Persistence;
using Stormbreaker.Web.Mvc;

namespace EckeSnuff {
    public class MvcApplication : System.Web.HttpApplication {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute("Eckesnuff_Default", "", new { controller = "Home", action = "Index", pagePath = "Home" });

        }
        private static void RegisterViewEngines(ICollection<IViewEngine> engines) {
            engines.Add(new WebFormViewEngine {
                                                  AreaViewLocationFormats = new[] { "~/Packages/{2}/Views/{1}/{0}.aspx" },
                                                  AreaMasterLocationFormats = new[] { "~/Packages/{2}/Views/Shared/{0}.aspx" },
                                                  AreaPartialViewLocationFormats = new[] { "~/Packages/{2}/Views/{1}/{0}.ascx", "~/Packages/{2}/Views/Shared/{0}.ascx" }
                                              });
        }
        protected void Application_Start() {
            //AreaRegistration.RegisterAllAreas();

            //// TODO fix priority for routes
            var area1Reg = new DashboardAreaRegistration();
            var area1Context = new AreaRegistrationContext(area1Reg.AreaName, RouteTable.Routes);
            area1Reg.RegisterArea(area1Context);

            //var area2Reg = new BlogAreaRegistration();
            //var area2Context = new AreaRegistrationContext(area2Reg.AreaName, RouteTable.Routes);
            //area2Reg.RegisterArea(area2Context);

            var area3Reg = new EckeSnuffRegistration();
            var area3Context = new AreaRegistrationContext(area3Reg.AreaName, RouteTable.Routes);
            area3Reg.RegisterArea(area3Context);



            Bootstrapper.Bootstrap();
            RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            ModelMetadataProviders.Current = new SortOrderDataAnnotationsModelMetadataProvider();
            RegisterViewEngines(ViewEngines.Engines);

        }
    }
}