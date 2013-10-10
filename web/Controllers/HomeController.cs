using System.Configuration;
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

        public ActionResult Index(Home currentPage) {
            var t = new ContentViewModel(currentPage)
            {NavigationContext = _structureInfo.NavigationContext, Title = ConfigurationManager.AppSettings["SiteName"]};
            return
                View(t);
        }

    }
}