using BrickPile.UI;
using EckeSnuff.Models;


namespace EckeSnuff.ViewModels {
    public class ContentViewModel : BaseViewModel<BaseModel> {
        public ContentViewModel(BaseModel model, IStructureInfo structureInfo) : base(model, structureInfo) {}
    }
}