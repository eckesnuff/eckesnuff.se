using System;
using System.IO;
using System.Web;
using EckeSnuff.Dropbox.Hosting;
using Quartz;

namespace EckeSnuff.Dropbox {
    class UpdateLocalFiles : IStatefulJob {
        private DropboxVirtualPathProvider _provider;
        #region public void Execute(JobExecutionContext context)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
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
        #endregion
        #region protected void GetAndUpdateFiles(DirectoryInfo dir)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
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
            foreach (var children in dir.GetDirectories()) {
                GetAndUpdateFiles(children);
            }
        }
        #endregion
        #region protected string TrimFilePath(string path)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected string TrimFilePath(string path) {
            path = path.Replace(_provider.PhysicalPath, VirtualPathUtility.RemoveTrailingSlash(_provider.VirtualPath));
            return path.Replace("\\", "/");
        }
        #endregion
        #region protected bool IsDirty(DropboxVirtualFile dbFile, FileInfo fileOnDisk)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbFile"></param>
        /// <param name="fileOnDisk"></param>
        /// <returns>True if it is dirty, otherwise false.</returns>
        protected bool IsDirty(DropboxVirtualFile dbFile, FileInfo fileOnDisk) {
            return dbFile.Changed > fileOnDisk.LastWriteTime;
        }
        #endregion
    }
}
