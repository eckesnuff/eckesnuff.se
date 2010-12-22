using System.Web.Mvc;

namespace EckeSnuff {
    public class EckeSnuffRegistration : AreaRegistration {

        public override string AreaName {
            get {
                return "EckeSnuff.Web";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "Ajax_Handler",
                "ajax/{action}/{ticks}",
                new { controller = "Ajax", action = "index", ticks= "" }
                );
            context.MapRoute(
            "Guestbook",
            "gb/{action}/{page}/{async}",
            new { controller = "GuestBook", action = "index", pagePath="gb",page="1",async=false});
            context.MapRoute(
                "Page_Home",
                "",
                new { controller = "Home", action = "index", pagePath= "home" });

            context.MapRoute(
                "Page_Default",
                "{*pagePath}",
                new { controller = "Page", action = "index", pagePath= "" }
                );


        }
    }
}