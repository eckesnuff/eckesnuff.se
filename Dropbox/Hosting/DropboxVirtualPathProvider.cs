using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Permissions;
using System.Web;
using System.Web.Hosting;
using EPiServer.Web.Hosting;

namespace EckeSnuff.Dropbox.Hosting {
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.High)]
    public class DropboxVirtualPathProvider : VirtualPathProvider {
        private readonly IDropboxService _service;
        //public DropboxVirtualPathProvider(string name, NameValueCollection config)
        //    : base(name, config) {
        //    _service=
        //   new DropboxService(ConfigurationManager.AppSettings["DropboxAppKey"],
        //                       ConfigurationManager.AppSettings["DropboxAppSecret"],
        //                       ConfigurationManager.AppSettings["DropboxUserName"],
        //                       ConfigurationManager.AppSettings["DropboxPassword"]);
        //    ValidateAndSetupConfigParams();
        //}
        public DropboxVirtualPathProvider(IDropboxService service) {
            _service=service;
        }
        /// <summary>
        /// Gets the physical path.
        /// </summary>
        public string PhysicalPath {
            get {
                return ConfigurationManager.AppSettings["DropboxPhysicalPath"];
            }
        }
        /// <summary>
        /// Gets the virtual path.
        /// </summary>
        public string VirtualPath {
            get { return ConfigurationManager.AppSettings["DropboxVirtualPath"]; }
        }
        public string RootFolder {
            get { return ConfigurationManager.AppSettings["DropboxRootFolder"]; }
        }
        /// <summary>
        /// Gets a value that indicates whether a file exists in the virtual file system.
        /// </summary>
        /// <param name="virtualPath">The path to the virtual file.</param>
        /// <returns>
        /// true if the file exists in the virtual file system; otherwise, false.
        /// </returns>
        public override bool FileExists(string virtualPath) {
            if (IsPathVirtual(virtualPath)) {
                var file = (DropboxVirtualFile)GetFile(virtualPath);
                return file.Exists;
            }
            return Previous.FileExists(virtualPath);
        }
        /// <summary>
        /// Gets a virtual file from the virtual file system.
        /// </summary>
        /// <param name="virtualPath">The path to the virtual file.</param>
        /// <returns>
        /// A descendent of the <see cref="T:System.Web.Hosting.VirtualFile"/> class that represents a file in the virtual file system.
        /// </returns>
        public override VirtualFile GetFile(string virtualPath) {
            if (IsPathVirtual(virtualPath)) {
                return new DropboxVirtualFile(virtualPath, this, _service);
            }
            return Previous.GetFile(virtualPath);
        }
        public override VirtualDirectory GetDirectory(string virtualPath) {
            if (IsPathVirtual(virtualPath)) {
                return new DropboxVirtualDirectory(virtualPath, this, _service);
            }
            return Previous.GetDirectory(virtualPath);
        }
        /// <summary>
        /// Determines whether [is path virtual] [the specified virtual path].
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns>
        ///   <c>true</c> if [is path virtual] [the specified virtual path]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsPathVirtual(string virtualPath) {
            if(string.IsNullOrEmpty(virtualPath))
                return false;
            var checkPath = VirtualPathUtility.ToAppRelative(virtualPath);
            return checkPath.StartsWith(VirtualPathUtility.Combine(VirtualPath,"public/"), StringComparison.InvariantCultureIgnoreCase);
        }
        public string GetPath(string virtualPath) {
            //return VirtualPathUtility.Combine("/" + RootFolder + "/",
            //                                  VirtualPathUtility.MakeRelative(VirtualPath, virtualPath));
            return "/"+VirtualPathUtility.MakeRelative(VirtualPath, virtualPath);
        }
        public override bool DirectoryExists(string virtualDir) {
            if (IsPathVirtual(virtualDir)) {
                var dir = new DropboxVirtualDirectory(virtualDir, this, _service);
                return dir.Exists;
            }
            return Previous.DirectoryExists(virtualDir);
        }
    }
}