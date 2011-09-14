using System;
using System.IO;
using System.Security.Permissions;
using System.Web;
using System.Web.Hosting;
using DropNet.Models;

namespace EckeSnuff.Dropbox.Hosting {
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class DropboxVirtualFile : VirtualFile {
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


        public virtual DateTime Changed { get { return MetaData.ModifiedDate; } }
        public virtual string Extension { get { return MetaData.Extension; } }
        public virtual string FileSize { get { return MetaData.Size; } }
        public virtual long Size { get { return MetaData.Bytes; } }
        public string PublicUrl { get { return MetaData.Path; } }


        #region public override string Name
        /// <summary>
        /// Gets the Name of the DropboxVirtualFile
        /// </summary>
        /// <value></value>
        public override string Name {
            get {
                return HttpUtility.UrlDecode(base.Name);
            }
        }
        #endregion
        #region public string PhysicalPath
        /// <summary>
        /// Gets the local path of the virtual file.
        /// </summary>
        /// <value></value>
        public string PhysicalPath {
            get {
                if (string.IsNullOrEmpty(Name)) {
                    throw new ArgumentException("Name cannot be null or empty");
                }
                return
                    Path.Combine(
                        _provider.PhysicalPath,
                        VirtualPathUtility.MakeRelative(_provider.VirtualPath, VirtualPath).Replace("/", "\\"));
            }
        }
        #endregion
        #region public bool Exists
        /// <summary>
        /// Gets a value indicating whether this <see cref="DropboxVirtualFile"/> is exists.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if exists; otherwise, <c>false</c>.
        /// </value>
        public bool Exists {
            get {
                // Return true if the file exists on disk
                if (File.Exists(PhysicalPath)) {
                    return true;
                }
                try {
                    return !string.IsNullOrEmpty(MetaData.Path)&&!MetaData.Is_Dir;
                }
                catch (Exception) {
                    return false;
                }
            }
        }
        
        #endregion
        #region public override Stream Open()
        /// <summary>
        /// When overridden in a derived class, returns a read-only stream to the virtual resource.
        /// </summary>
        /// <returns>
        /// A read-only stream to the virtual file.
        /// </returns>
        public override Stream Open() {
            if (File.Exists(PhysicalPath)) {
                return new FileStream(PhysicalPath, FileMode.Open, FileAccess.Read);
            }
            var buffer = _dropboxService.GetFile(_provider.GetPath(VirtualPath));
            var directory = new FileInfo(PhysicalPath).Directory;
            if (!directory.Exists)
                directory.Create();
            using (var fileStream = new FileStream(PhysicalPath, FileMode.Create, FileAccess.Write)) {
                var writer = new BinaryWriter(fileStream);
                writer.Write(buffer);
                writer.Close();
            }
            return new FileStream(PhysicalPath, FileMode.Open, FileAccess.Read);
        }
        #endregion
        #region public DropboxVirtualFile(string virtualPath, DropboxVirtualPathProvider provider,IDropboxService dropboxService)
        /// <summary>
        /// Initializes a new instance of the <see cref="DropboxVirtualFile"/> class.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="dropboxService"></param>
        public DropboxVirtualFile(string virtualPath, DropboxVirtualPathProvider provider, IDropboxService dropboxService)
            : base(virtualPath) {
            _dropboxService = dropboxService;
            _provider = provider;
        }
        #endregion
    }
}