using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Dropbox.Hosting;
using Dropbox.Sync;

namespace EckeSnuff {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            HostingEnvironment.RegisterVirtualPathProvider(new DropboxVirtualPathProvider());
            new DropboxSync().SetupScheduler(new Settings
            {
                DocumentIcon = "~/areas/ui/content/images/document.png",
                IntervalInSeconds = 10
            });
        }
    }
}