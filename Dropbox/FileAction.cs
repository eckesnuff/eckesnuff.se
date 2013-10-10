using System.Collections.Generic;
using DropNet.Models;

namespace Dropbox {
    public class FileAction {
        public string DeltaCursor { get; set; }
        public FileAction() {
            PathsForDeletion = new List<string>();
            FilesForCreationOrUpdate = new List<DeltaEntry>();
        }
        public List<string> PathsForDeletion { get; set; }
        public List<DeltaEntry> FilesForCreationOrUpdate { get; set; }

    }
}