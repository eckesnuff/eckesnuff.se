using System;
using System.Web.Mvc;
using BrickPile.Domain.Models;
using BrickPile.UI;
using EckeSnuff.Models;
using EckeSnuff.ViewModels;
using Raven.Client;

namespace EckeSnuff.Controllers
{
    public class GuestBookController : BaseController {
        private readonly IDocumentSession _documentSession;
        private readonly IStructureInfo _structureInfo;
        private readonly GuestBook _currentPage;

        public GuestBookController(IDocumentSession documentSession, IStructureInfo structureInfo,IPage currentPage) {
            _currentPage = currentPage as GuestBook;
            _documentSession = documentSession;
            _structureInfo = structureInfo;
        }
        public ActionResult Index(GuestBook currentPage, int? page) {
            var children = _documentSession.Load<Comment>(currentPage.Children);
            //var children = _pageRepository.GetChildren(model).OfType<Comment>();
            //foreach(var child in children) {
            //    child.Metadata.Slug = child.Id;
            //    child.Metadata.Url=child.Id;
            //    _pageRepository.Delete(child);
            //}
            //_pageRepository.SaveChanges();

            var viewModel = new GuestBookViewModel(currentPage, children) { NavigationContext = _structureInfo.NavigationContext, CurrentPage = page.GetValueOrDefault(0) };
            if (!HttpContext.Request.IsAjaxRequest())
                return View(viewModel);
            return PartialView("CommentList", viewModel);
        }
        
        [HttpPost]
        public ActionResult Comment([Bind(Prefix = "CurrentComment")] Comment model,string pagePath) {
            if (!ModelState.IsValid) {
                var children = _documentSession.Load<Comment>(model.Children);
                return
                    View("Index", new GuestBookViewModel(_currentPage, children) { CurrentComment = model });
            }
            model.Parent = _currentPage;
            model.Metadata.Name = model.Author + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            model.PublishDate=DateTime.Now;
            model.Metadata.Slug = DateTime.Now.Ticks.ToString();
            model.Metadata.Url = model.Metadata.Slug;
            _documentSession.Store(model);
            _documentSession.SaveChanges();
            UpdateModel(_currentPage);
            return RedirectToAction("index", new { model=_currentPage });
        }
    }

}
