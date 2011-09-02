using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace EckeSnuff.Controllers.Properties
{
    public class FileBrowserController : Controller
    {
        //
        // GET: /FileBrowser/

        public ActionResult Open() {
            var dropbox = HostingEnvironment.VirtualPathProvider.GetDirectory("~/dropbox/");
            return PartialView(dropbox);
        }

    }
}
