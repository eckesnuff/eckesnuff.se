using System;
using System.IO;
using System.Web;
using BrickPile.Core.Hosting;
using DropNet.Models;

namespace Dropbox.Hosting {
    public class DropboxVirtualDirectory : CommonVirtualDirectory {
        private readonly DropboxVirtualPathProvider _provider;
        private readonly string _relativePath;

        public DropboxVirtualDirectory(DropboxVirtualPathProvider provider, string virtualPath)
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

        public override System.Collections.IEnumerable Directories {
            get {
                foreach (var content in MetaData.Contents) {
                    if (content.Is_Dir) {
                        yield return
                            new DropboxVirtualDirectory(_provider,
                                                        VirtualPathUtility.AppendTrailingSlash(VirtualPath) +
                                                        content.Name);
                    }
                }
            }
        }

        public override System.Collections.IEnumerable Files {
            get {
                foreach (var content in MetaData.Contents) {
                    if (!content.Is_Dir) {
                        yield return
                            new DropboxVirtualFile(_provider,
                                                   VirtualPathUtility.AppendTrailingSlash(VirtualPath) + content.Name)
                            {MetaData = content};
                    }
                }
            }
        }

        public override System.Collections.IEnumerable Children {
            get {
                yield return Directories;
                yield return Files;
            }
        }

        public override CommonVirtualDirectory Parent {
            get {
                var index = _relativePath.LastIndexOf('/');
                if (index > -1) {
                    var parent = VirtualPath.Substring(0, VirtualPath.LastIndexOf('/') + 1);
                    return new DropboxVirtualDirectory(_provider, parent);
                }
                return null;
            }
        }

        public override CommonVirtualDirectory CreateDirectory(string name) {
            var newPath = Path.Combine(VirtualPath, name);
            var metaData =_provider.DropBoxClient.CreateFolder(_provider.MakeRelative(newPath));
            return new DropboxVirtualDirectory(_provider, newPath) {MetaData = metaData};
        }

        public override CommonVirtualFile CreateFile(string name) {
            var metaData = _provider.DropBoxClient.UploadFile(_relativePath, name, new byte[] {});
            var newPath = Path.Combine(VirtualPath, name);
            return new DropboxVirtualFile(_provider, newPath) {MetaData = metaData};

        }
        public bool Exists {
            get {
                try {
                    return !MetaData.Is_Deleted && MetaData.Is_Dir;
                }
                catch (Exception) {
                    return false;
                }
            }
        }
    }
}