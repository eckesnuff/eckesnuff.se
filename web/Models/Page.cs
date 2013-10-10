using BrickPile.Domain;
using EckeSnuff.Controllers;

namespace EckeSnuff.Models {
    [ContentType(Name = "Standard page",ControllerType = typeof(PageController))]
    public class StandardPage : BaseModel {
    }

}
