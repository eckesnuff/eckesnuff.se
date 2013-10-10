using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BrickPile.Domain.Models;

namespace EckeSnuff.Models {
    public class BaseModel:BrickPile.Domain.Models.Page {
        [DisplayName("Heading:")]
        //[Required(ErrorMessage="Required")]
        public virtual string Heading { get; set; }

        [DataType(DataType.Html)]
        [DisplayName("Content:")]
        //[Required(ErrorMessage="Required field")]
        public virtual string Content { get; set; }
    }
}