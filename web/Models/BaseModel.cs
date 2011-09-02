using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BrickPile.Domain.Models;

namespace EckeSnuff.Models {
    public class BaseModel:PageModel {
        [DisplayName("Heading:")]
        [Required(ErrorMessage="Required")]
        public virtual string Heading { get; set; }

        [DataType(DataType.Html)]
        [DisplayName("Content:")]
        [Required(ErrorMessage="Required field")]
        public virtual string Content { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Meta description:")]
        public virtual string MetaDescription { get; set; }
    }
}