using BrickPile.Domain;
using EckeSnuff.Controllers;

namespace EckeSnuff.Models {
    [ContentType(Name= "Guestbook", ControllerType=typeof(GuestBookController))]
    public class GuestBook:BaseModel {
    }
}