using System;
using System.Collections;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Hosting;
using DropNet.Models;

namespace EckeSnuff.Dropbox.Hosting {
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class DropboxVirtualDirectory : VirtualDirectory {
        private readonly DropboxVirtualPathProvider _provider;
        private readonly IDropboxService _dropboxService;
        #region internal MetaData MetaData
        /// <summary>
        /// Get/Sets the MetaData of the DropboxVirtualFile
        /// </summary>
        /// <value></value>
        internal MetaData MetaData {
            get {
                if (_metaData==null)
                    _metaData = _dropboxService.GetMetadata(_provider.GetPath(VirtualPath));
                return _metaData;
            }
            set {
                _metaData=value;
            }
        }
        private MetaData _metaData;
        #endregion

        public bool Exists {
            get {
                try {
                    return !string.IsNullOrEmpty(MetaData.Path);
                }
                catch (Exception ex) {
                    return false;
                }
            }
        }
        public override IEnumerable Directories {
            get { return GetDirectories(); } 
        }

        public override IEnumerable Files {
            get { return GetFiles(); }
        }

        public override IEnumerable Children {
            get {
                var list = Directories.Cast<object>().ToList();
                list.AddRange(Files.Cast<object>());
                return list;
            }
        }
        private IEnumerable GetFiles() {
            if(MetaData.Contents==null)
                MetaData = _dropboxService.GetMetadata(VirtualPath);
            foreach(var file in MetaData.Contents) {
                if (!file.Is_Dir) {
                    yield return new DropboxVirtualFile(file.Path, _provider, _dropboxService) {MetaData = file};
                }
            }
        }
        private IEnumerable GetDirectories() {
            if(MetaData.Contents==null)
                MetaData = _dropboxService.GetMetadata(VirtualPath);
            foreach (var dir in MetaData.Contents) {
                if (dir.Is_Dir) {
                    yield return new DropboxVirtualDirectory(dir.Path, _provider, _dropboxService) {MetaData = dir};
                }
            }
        }
        public DropboxVirtualDirectory(string virtualPath, DropboxVirtualPathProvider provider, IDropboxService dropboxService) : base(virtualPath) {
            _dropboxService = dropboxService;
            _provider = provider;
        }
    }
}