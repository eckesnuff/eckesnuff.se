using System.Web.Mvc;
using BrickPile.UI;
using EckeSnuff.Models;
using EckeSnuff.ViewModels;
namespace EckeSnuff.Controllers
{
    public class PageController : BaseController {
        private readonly IStructureInfo _structureInfo;

        public PageController(IStructureInfo structureInfo) {
            _structureInfo = structureInfo;
        }

        public ActionResult Index(StandardPage currentPage) {
            var t =new ContentViewModel(currentPage) { NavigationContext = _structureInfo.NavigationContext };
            return View(t);
        }
    }
}
