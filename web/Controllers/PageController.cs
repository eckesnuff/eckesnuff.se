using System.Web.Mvc;
using EckeSnuff.ViewModels;
using Stormbreaker.Services;


namespace EckeSnuff.Controllers
{
    public class PageController : BaseController {
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
        public PageController(IStormbreakerServices services):base(services) {
            Services = services;
        }
        #endregion

        #region public ActionResult Index()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string pagePath) {
            return View(new ContentModel(Services.ContentManager.GetByUrlSegment(pagePath), RootPages, Tweets, Flickr));
        }
        #endregion

    }
}
