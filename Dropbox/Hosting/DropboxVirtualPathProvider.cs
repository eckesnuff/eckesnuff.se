using System;
using System.Configuration;
using System.Security.Permissions;
using System.Web;
using System.Web.Hosting;
using BrickPile.Core.Hosting;
using DropNet;

namespace Dropbox.Hosting {
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.High)]
    public class DropboxVirtualPathProvider : CommonVirtualPathProvider {
        public DropNetClient DropBoxClient;

        public DropboxVirtualPathProvider() {
            DropBoxClient = new DropNetClient(AppAccessDropBox.ConsumerKey,
                                              AppAccessDropBox.ConsumerSecret,
                                              AppAccessDropBox.AccessToken) {UseSandbox = true};
        }

        public override string PhysicalPath {
            get { return string.Empty; }
        }

        public override string VirtualPathRoot {
            get {
                return
                    VirtualPathUtility.AppendTrailingSlash(
                        ConfigurationManager.AppSettings["VirtualPathRoot"]);

            }
        }

        public string MakeRelative(string path) {
            return HttpUtility.UrlDecode("/" + VirtualPathUtility.MakeRelative(VirtualPathRoot, path));
        }



        public override bool FileExists(string virtualPath) {
            if (IsPathVirtual(virtualPath)) {
                return new DropboxVirtualFile(this, virtualPath).Exists;
            }
            return base.FileExists(virtualPath);
        }

        public override bool DirectoryExists(string virtualDir) {
            if (IsPathVirtual(virtualDir)) {
                return new DropboxVirtualFile(this, virtualDir).Exists;
            }
            return base.DirectoryExists(virtualDir);
        }

        public override VirtualDirectory GetDirectory(string virtualDir) {
            if (IsPathVirtual(virtualDir)) {
                return new DropboxVirtualDirectory(this, virtualDir);
            }
            return Previous.GetDirectory(virtualDir);
        }

        public override VirtualFile GetFile(string virtualPath) {
            if (IsPathVirtual(virtualPath)) {
                return new DropboxVirtualFile(this, virtualPath);
            }
            return Previous.GetFile(virtualPath);
        }
        private bool IsPathVirtual(string virtualPath) {
            if (string.IsNullOrEmpty(virtualPath))
                return false;
            var checkPath = VirtualPathUtility.ToAppRelative(virtualPath);
            return checkPath.StartsWith(VirtualPathRoot, StringComparison.InvariantCultureIgnoreCase);
        }

        private static class AppAccessDropBox {
            public static string ConsumerKey = ConfigurationManager.AppSettings["DropboxConsumerKey"];
            public static string ConsumerSecret = ConfigurationManager.AppSettings["DropboxConsumerSecret"];
            public static string AccessToken = ConfigurationManager.AppSettings["DropboxAccessToken"];
        }
    }
}