using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BrickPile.Domain;
using BrickPile.Domain.Models;

namespace EckeSnuff.Models {
    [PageModel("Comment", Description="Comment")]
    public class Comment : PageModel {
        #region public virtual DateTime PublishDate
        /// <summary>
        /// Get/Sets the PublishDate of the Comment
        /// </summary>
        /// <value></value>
        [ScaffoldColumn(false)]
        public virtual DateTime PublishDate {
            get;
            set;
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
            get;
            set;
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
            get;
            set;
        }
        #endregion
    }
}