using System.Web.Hosting;
using System.Web.Mvc;

namespace EckeSnuff.Controllers.Properties
{
    [Authorize]
    public class FileBrowserController : Controller
    {
        //
        // GET: /FileBrowser/

        public ActionResult Open() {
            var dropbox = HostingEnvironment.VirtualPathProvider.GetDirectory("~/dropbox/public/");
            return PartialView(dropbox);
        }
        public ActionResult GetContent(string path) {
            var dropbox = HostingEnvironment.VirtualPathProvider.GetDirectory("~/dropbox" + path);
            return PartialView("_FileLister", dropbox);
        }
        public ActionResult OpenTinyMceDialog() {
            var dropbox = HostingEnvironment.VirtualPathProvider.GetDirectory("~/dropbox/public/");
            return View(dropbox);
        }


    }
}
