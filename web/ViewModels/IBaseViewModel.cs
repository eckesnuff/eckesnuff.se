using System.Collections.Generic;
using BrickPile.UI.Web.ViewModels;
using EckeSnuff.Entities;
using TweetSharp;

namespace EckeSnuff.ViewModels {
    /// <summary>
    /// Summary description for IBaseViewModel.
    /// </summary>
    /// <remarks>
    /// 2011-07-19 erik: Created 
    /// </remarks>
    public interface IBaseViewModel<out TModel> : IViewModel<TModel> {
        /* *******************************************************************
		 *  Properties 
		 * *******************************************************************/
        string Title { get; set; }
        IList<FlickrItem> Flickr { get; set; }
        IEnumerable<TwitterStatus> Tweets { get; set; }
        /* *******************************************************************
		 *  Methods 
		 * *******************************************************************/

        /* *******************************************************************
		 *  Events 
		 * *******************************************************************/
    }
}