using System;
using System.IO;
using BrickPile.Core.Hosting;
using DropNet.Models;

namespace Dropbox.Hosting {
    public class DropboxVirtualFile:CommonVirtualFile {
        private readonly DropboxVirtualPathProvider _provider;
        private readonly string _relativePath;

        public DropboxVirtualFile(DropboxVirtualPathProvider provider, string virtualPath)
            : base(virtualPath) {
            _provider = provider;
            _relativePath = provider.MakeRelative(virtualPath);
        }

        #region internal MetaData MetaData

        /// <summary>
        /// Get/Sets the MetaData of the DropboxVirtualDirectory
        /// </summary>
        /// <value></value>
        internal MetaData MetaData {
            get {
                if (_metaData == null)
                    _metaData = _provider.DropBoxClient.GetMetaData(_relativePath);
                return _metaData;
            }
            set { _metaData = value; }
        }

        private MetaData _metaData;

        #endregion

        public override string Url {
            get { return VirtualPath.ToLower(); }
        }
        public override Stream Open() {
            return Open(FileMode.Open);
        }
        public override Stream Open(FileMode fileMode) {
            if (fileMode == FileMode.Open) {
                return new MemoryStream(_provider.DropBoxClient.GetFile(_relativePath));
            }
            if (fileMode == FileMode.Create || fileMode == FileMode.OpenOrCreate) {
                var tempPath = Path.GetTempFileName();
                var fileStream = new CustomFileStream(tempPath, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                                                      FileShare.None,
                                                      () => {
                                                          using (var fs = File.OpenRead(tempPath)) {
                                                              _provider.DropBoxClient.UploadFile(
                                                                  GetFolderPath(_metaData.Path),
                                                                  MetaData.Name, fs);
                                                          }
                                                          File.Delete(tempPath);
                                                      });
                return fileStream;
            }
            return null;
        }

        private string GetFolderPath(string path) {
            var index = path.LastIndexOf('/');
            if(index>0) {
                return path.Substring(0, index + 1);
            }
            return string.Empty;
        }
        public override void Delete() {
            try { //TODO: REMOVE TRY Catch
                _provider.DropBoxClient.Delete(_relativePath);
            }
            catch (Exception) {

                return;
            }

        }

        public bool Exists {
            get {
                try {
                    return !MetaData.Is_Deleted && !MetaData.Is_Dir;
                }
                catch (Exception) {
                    return false;
                }
            }
        }

        public Uri GetSharedUrl() {
            var shareResponse = _provider.DropBoxClient.GetShare(_relativePath, false);
            return new Uri(shareResponse.Url);
        }

        public override string LocalPath {
            get { throw new NotImplementedException(); }
        }
    }
}
