using System.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using BrickPile.UI;
using EckeSnuff.Models;
using EckeSnuff.ViewModels;

namespace EckeSnuff.Controllers {
    public class HomeController : BaseController {
        private readonly IStructureInfo _structureInfo;

        public HomeController(IStructureInfo structureInfo) {
            _structureInfo = structureInfo;
        }

        public ActionResult Index(Home model) {            
            return
                View(new ContentViewModel(model, _structureInfo) {Title = ConfigurationManager.AppSettings["SiteName"]});
        }

    }
}