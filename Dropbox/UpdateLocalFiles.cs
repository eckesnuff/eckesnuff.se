using System;
using System.IO;
using System.Web;
using EckeSnuff.Dropbox.Hosting;
using Quartz;

namespace EckeSnuff.Dropbox {
    class UpdateLocalFiles : IStatefulJob {
        DropboxVirtualPathProvider _provider = null;
        public void Execute(JobExecutionContext context) {
            _provider = context.JobDetail.JobDataMap.Get("Provider") as DropboxVirtualPathProvider;
            if (_provider==null) {
                throw new ArgumentException("Provider must be of type DropboxVirtualPathProvider");
            }
            var root = new DirectoryInfo(_provider.PhysicalPath);
            if (!root.Exists)
                root.Create();
            GetAndUpdateFiles(root);
        }
        protected void GetAndUpdateFiles(DirectoryInfo dir) {
            foreach (FileInfo file in dir.GetFiles()) {
                var dropBoxFile = _provider.GetFile(TrimFilePath(file.FullName)) as DropboxVirtualFile;
                if (dropBoxFile==null || !dropBoxFile.Exists) {
                    continue;
                }
                if (IsDirty(dropBoxFile, file)) {
                    file.Delete();
                }
            }
            foreach(var children in dir.GetDirectories()) {
                GetAndUpdateFiles(children);
            }
        }

        protected string TrimFilePath(string path) {
            path = path.Replace(_provider.PhysicalPath, VirtualPathUtility.RemoveTrailingSlash(_provider.VirtualPath));
            return path.Replace("\\", "/");
        }
        protected bool IsDirty(DropboxVirtualFile dbFile, FileInfo fileOnDisk) {
            return dbFile.Changed > fileOnDisk.LastWriteTimeUtc;
        }
    }
}
