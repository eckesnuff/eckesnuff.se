using System.ComponentModel.DataAnnotations;
using BrickPile.Domain;

namespace EckeSnuff.Models {
    [PageModel("ImageGallery")]
    public class ImageGallery:BaseModel {
        [UIHint("FileManager")]
        [Display(Name="DropboxPath")]
        public string VirtualPath {
            get;
            set;
        }
    }
}