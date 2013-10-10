using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using BrickPile.UI;
using Dropbox.Hosting;
using EckeSnuff.Models;

namespace EckeSnuff.Controllers
{
    public class ImageGalleryController : BaseController
    {
                private readonly IStructureInfo _structureInfo;

                public ImageGalleryController(IStructureInfo structureInfo) {
            _structureInfo = structureInfo;
        }
        //
        // GET: /ImageGallery/

                public ActionResult Index(ImageGallery currentPage) {

                    var virtualPathProvider = HostingEnvironment.VirtualPathProvider as DropboxVirtualPathProvider;
                    var root = virtualPathProvider.GetDirectory(virtualPathProvider.VirtualPathRoot);


                    var directory = System.Web.Hosting.HostingEnvironment.VirtualPathProvider.GetDirectory("~/dropbox/");

                    GetDir((DropboxVirtualDirectory) directory);
                    return View(directory);
                }

                public void GetDir(DropboxVirtualDirectory dir) {

                    Debug.WriteLine("DIR: " + dir.Name + " " + dir.Exists + " " + dir.VirtualPath);
                    if (!dir.Exists) return;
                    var files = dir.Files.OfType<DropboxVirtualFile>().ToList();
                    Debug.WriteLine("FILES");
                    foreach (var dropboxVirtualFile in files) {

                        Debug.WriteLine(dropboxVirtualFile.Name + " " + dropboxVirtualFile.Exists + " " +
                                        dropboxVirtualFile.VirtualPath);
                    }
                    var dirs = dir.Directories.OfType<DropboxVirtualDirectory>().ToList();
                    foreach (var dropboxVirtualDirectory in dirs) {
                        GetDir(dropboxVirtualDirectory);
                    }
                }
    }
}
