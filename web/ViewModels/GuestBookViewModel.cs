using System.Collections.Generic;
using BrickPile.UI;
using EckeSnuff.Models;


namespace EckeSnuff.ViewModels {
    public class GuestBookViewModel :BaseViewModel<GuestBook> {
        public GuestBookViewModel(GuestBook model, IStructureInfo structureInfo, IEnumerable<Comment> comments)
            : base(model, structureInfo) {
            Comments = comments;
        }

        public IEnumerable<Comment> Comments {
            get; private set;
        }
        public Comment CurrentComment {
            get; set;
        }
        public int CurrentPage {
            get; set;
        }
    }
}
