using System.Web.Hosting;
using System.Web.Mvc;

namespace EckeSnuff.Controllers
{
    public class TestVPPController : Controller
    {
        //
        // GET: /TestVPP/

        public ActionResult Index()
        {
            return View(HostingEnvironment.VirtualPathProvider.GetDirectory("~/dropbox/eckesnuff.se/vinter"));
        }

    }
}
