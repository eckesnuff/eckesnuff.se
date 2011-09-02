using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using BrickPile.Core.Repositories;
using BrickPile.Domain.Models;
using BrickPile.UI;
using EckeSnuff.Models;
using EckeSnuff.ViewModels;

namespace EckeSnuff.Controllers
{
    public class GuestBookController : BaseController {
        private readonly IPageRepository _pageRepository;
        private readonly IStructureInfo _structureInfo;
        private readonly GuestBook _currentPage;

        public GuestBookController(IPageRepository pageRepository, IStructureInfo structureInfo,IPageModel currentPage) {
            _currentPage = currentPage as GuestBook;
            _pageRepository = pageRepository;
            _structureInfo = structureInfo;
        }
        public ActionResult Index(GuestBook model, int? page) {
            var children = _pageRepository.GetChildren(model).OfType<Comment>();
            //foreach(var child in children) {
            //    child.Metadata.Slug = child.Id;
            //    child.Metadata.Url=child.Id;
            //    _pageRepository.Delete(child);
            //}
            //_pageRepository.SaveChanges();

            var viewModel = new GuestBookViewModel(model, _structureInfo, children) { CurrentPage = page.GetValueOrDefault(0) };
            if (!HttpContext.Request.IsAjaxRequest())
                return View(viewModel);
            return PartialView("CommentList", viewModel);
        }
        
        [HttpPost]
        public ActionResult Comment([Bind(Prefix = "CurrentComment")] Comment model,string pagePath) {
            if (!ModelState.IsValid) {
                return
                    View("Index", new GuestBookViewModel(_currentPage, _structureInfo, _pageRepository.GetChildren(_currentPage).OfType<Comment>()) { CurrentComment = model });
            }
            model.Parent = _currentPage;
            model.Metadata.Name = model.Author + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            model.PublishDate=DateTime.Now;
            model.Metadata.Slug = DateTime.Now.Ticks.ToString();
            model.Metadata.Url = model.Metadata.Slug;
            _pageRepository.Store(model);
            _pageRepository.SaveChanges();
            UpdateModel(_currentPage);
            return RedirectToAction("index", new { model=_currentPage });
        }
    }

}
