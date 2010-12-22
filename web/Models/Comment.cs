using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Stormbreaker.Models;
using Stormbreaker.Web;

namespace EckeSnuff.Models {
    [PageType(Name="Comment", Description="Comment")]
    public class Comment : ContentItem {

        #region public override System.DateTime? StartPublish
        /// <summary>
        /// Get/Sets the StartPublish of the Comment
        /// </summary>
        /// <value></value>
        [ScaffoldColumn(false)]
        public override System.DateTime? StartPublish {
            get {
                return base.StartPublish;
            }
            set {
                base.StartPublish = value;
            }
        }
        #endregion
        #region public override System.DateTime? StopPublish
        /// <summary>
        /// Get/Sets the StopPublish of the Comment
        /// </summary>
        /// <value></value>
        [ScaffoldColumn(false)]
        public override System.DateTime? StopPublish {
            get {
                return base.StopPublish;
            }
            set {
                base.StopPublish = value;
            }
        }
        #endregion
        #region public override bool Visible
        /// <summary>
        /// Get/Sets the Visible of the Comment
        /// </summary>
        /// <value></value>
        [ScaffoldColumn(false)]
        public override bool Visible {
            get {
                return base.Visible;
            }
            set {
                base.Visible = value;
            }
        }
        #endregion
        #region public override string UrlSegment
        /// <summary>
        /// Get/Sets the UrlSegment of the Comment
        /// </summary>
        /// <value></value>
        [ScaffoldColumn(false)]
        public override string UrlSegment {
            get {
                return base.UrlSegment;
            }
            set {
                base.UrlSegment = value;
            }
        }
        #endregion
        #region public override string Name
        /// <summary>
        /// Get/Sets the Name of the Comment
        /// </summary>
        /// <value></value>
        [ScaffoldColumn(false)]
        public override string Name {
            get {
                return base.Name;
            }
            set {
                base.Name = value;
            }
        }
        #endregion

        #region public virtual string Author
        /// <summary>
        /// Get/Sets the Author of the Comment
        /// </summary>
        /// <value></value>
        [DataType(DataType.Text)]
        [Required(ErrorMessage="Please provide your name")]
        [DisplayName("Name (required)")]
        public virtual string Author {
            get { return GetDetail("Author") as string; }
            set { SetDetail("Author", value); }
        }
        #endregion
        #region public virtual string CommentContent
        /// <summary>
        /// Get/Sets the CommentContent of the Comment
        /// </summary>
        /// <value></value>
        [DataType(DataType.MultilineText)]
        [DisplayName("Comment (required)")]
        [Required(ErrorMessage="Please provide a comment")]
        public virtual string CommentContent {
            get { return GetDetail("CommentContent") as string; }
            set { SetDetail("CommentContent", value); }
        }
        #endregion
    }

}