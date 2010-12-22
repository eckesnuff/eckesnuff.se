using System.Web.Mvc;
using EckeSnuff.Models;
using EckeSnuff.ViewModels;
using Stormbreaker.Controllers;
using Stormbreaker.Models;
using Stormbreaker.Services;

namespace EckeSnuff.Controllers {
    public class HomeController : BaseController {
        #region public virtual IStormbreakerServices Services
        /// <summary>
        /// Get/Sets the Services of the HomeController
        /// </summary>
        /// <value></value>
        public virtual IStormbreakerServices Services { get; private set; }
        #endregion

        #region public HomeController(IStormbreakerServices services)
        /// <summary>
        /// Initializes a new instance of the <b>HomeController</b> class.
        /// </summary>
        /// <param name="services"></param>
        public HomeController(IStormbreakerServices services):base(services) {
            Services = services;
        }
        #endregion

        public ActionResult Index(string pagePath) {

            return View(new ContentModel(Services.ContentManager.GetByUrlSegment(pagePath),RootPages,Tweets,Flickr));
        }
    }
}