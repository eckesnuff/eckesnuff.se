using System.Web.Mvc;
using EckeSnuff.Services;
using EckeSnuff.ViewModels;

namespace EckeSnuff {
    public class PopulateViewModel : ActionFilterAttribute {

        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            var model = filterContext.Controller.ViewData.Model as IBaseViewModel<Models.BaseModel>;
            if(model!=null) {
                model.Tweets = Twitter.Instance.GetTweets(10);
                model.Flickr = Flickr.Instance.GetItems(12);
            }
        }
    }
}
