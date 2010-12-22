using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EckeSnuff.Models;
using EckeSnuff.ViewModels;
using Stormbreaker.Services;

namespace EckeSnuff.Controllers
{
    public class GuestBookController : BaseController {
        #region public virtual IStormbreakerServices Services
        /// <summary>
        /// Get/Sets the Services of the HomeController
        /// </summary>
        /// <value></value>
        public virtual IStormbreakerServices Services { get; private set; }
        #endregion

        #region public PageController(IStormbreakerServices services)
        /// <summary>
        /// Initializes a new instance of the <b>PageController</b> class.
        /// </summary>
        /// <param name="services"></param>
        public GuestBookController(IStormbreakerServices services)
            : base(services) {
            Services = services;
        }
        #endregion
        //
        // GET: /GuestBook/

        public ActionResult Index(string pagePath,int page,bool async) {
            var model = new GuestBookModel(Services.ContentManager.GetByUrlSegment(pagePath), RootPages, Tweets, Flickr);
            model.CurrentPage = page;
            if(!async)
                return View(model);
            return PartialView("CommentList", model);
        }
        
        [HttpPost]
        public ActionResult Comment([Bind(Prefix = "CurrentComment")] Comment comment,string pagePath) {
            ModelState.Remove("CurrentComment.Name");
            if(!this.ModelState.IsValid) {
                return
                    View("Index",new GuestBookModel(Services.ContentManager.GetByUrlSegment(pagePath), RootPages, Tweets, Flickr)
                         {CurrentComment = comment });
            }
            var guestBook = Services.ContentManager.GetByUrlSegment(pagePath);
 
            comment.Parent = guestBook;
            comment.Visible = true;
            comment.Name = comment.Author + comment.StartPublish;

            Services.ContentManager.Create(comment);
            UpdateModel(guestBook);
            return RedirectToAction("Index",new { pagePath = guestBook.UrlSegment });
        }

    }
}
