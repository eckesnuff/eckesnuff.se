using System;
using System.IO;

namespace Dropbox.Hosting {
    internal class CustomFileStream:FileStream {
        private readonly Action _upload;

        public CustomFileStream(string path, FileMode mode, FileAccess access, FileShare share,Action upload)
            : base(path, mode, access, share) {
            _upload = upload;
        }
        public override void Close() {
            base.Close();
            _upload();
        }
    }

}