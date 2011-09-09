using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrickPile.UI;
using EckeSnuff.Models;

namespace EckeSnuff.Controllers
{
    public class ImageGalleryController : BaseController
    {
                private readonly IStructureInfo _structureInfo;

                public ImageGalleryController(IStructureInfo structureInfo) {
            _structureInfo = structureInfo;
        }
        //
        // GET: /ImageGallery/

        public ActionResult Index(ImageGallery model) {
            var directory = System.Web.Hosting.HostingEnvironment.VirtualPathProvider.GetDirectory("~/dropbox/"+model.VirtualPath);
            return View(directory);
        }

    }
}
