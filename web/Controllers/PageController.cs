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

        public ActionResult Index(Page model) {
            return View(new ContentViewModel(model,_structureInfo));
        }
    }
}
