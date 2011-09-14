using System;
using System.Configuration;
using System.Security.Permissions;
using System.Web;
using System.Web.Hosting;
using Quartz;
using Quartz.Impl;

namespace EckeSnuff.Dropbox.Hosting {
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.High)]
    public class DropboxVirtualPathProvider : VirtualPathProvider {
        private readonly IDropboxService _service;
        #region public DropboxVirtualPathProvider(IDropboxService service)
        /// <summary>
        /// Initializes a new instance of the <b>DropboxVirtualPathProvider</b> class.
        /// </summary>
        /// <param name="service"></param>
        public DropboxVirtualPathProvider(IDropboxService service) {
            _service=service;
        }
        #endregion
        #region public string PhysicalPath
        /// <summary>
        /// Gets the physical path.
        /// </summary>
        /// <value></value>
        public string PhysicalPath {
            get {
                return ConfigurationManager.AppSettings["DropboxPhysicalPath"];
            }
        }
        #endregion
        #region public string VirtualPath
        /// <summary>
        /// Gets the virtual path.
        /// </summary>
        /// <value></value>
        public string VirtualPath {
            get { return ConfigurationManager.AppSettings["DropboxVirtualPath"]; }
        }
        #endregion
        #region public override bool FileExists(string virtualPath)
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
        #endregion
        #region public override VirtualFile GetFile(string virtualPath)
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
        #endregion
        #region public override VirtualDirectory GetDirectory(string virtualPath)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public override VirtualDirectory GetDirectory(string virtualPath) {
            if (IsPathVirtual(virtualPath)) {
                return new DropboxVirtualDirectory(virtualPath, this, _service);
            }
            return Previous.GetDirectory(virtualPath);
        }
        #endregion
        #region private bool IsPathVirtual(string virtualPath)
        /// <summary>
        /// Determines whether [is path virtual] [the specified virtual path].
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns>
        /// 	<c>true</c> if [is path virtual] [the specified virtual path]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsPathVirtual(string virtualPath) {
            if (string.IsNullOrEmpty(virtualPath))
                return false;
            var checkPath = VirtualPathUtility.ToAppRelative(virtualPath);
            return checkPath.StartsWith(VirtualPathUtility.Combine(VirtualPath, "public/"), StringComparison.InvariantCultureIgnoreCase);
        }
        #endregion
        #region public string GetPath(string virtualPath)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public string GetPath(string virtualPath) {
            //return VirtualPathUtility.Combine("/" + RootFolder + "/",
            //                                  VirtualPathUtility.MakeRelative(VirtualPath, virtualPath));
            return "/"+VirtualPathUtility.MakeRelative(VirtualPath, virtualPath);
        }
        #endregion
        #region public override bool DirectoryExists(string virtualDir)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualDir"></param>
        /// <returns></returns>
        public override bool DirectoryExists(string virtualDir) {
            if (IsPathVirtual(virtualDir)) {
                var dir = new DropboxVirtualDirectory(virtualDir, this, _service);
                return dir.Exists;
            }
            return Previous.DirectoryExists(virtualDir);
        }
        #endregion
        #region public void SetupScheduler()
        /// <summary>
        /// 
        /// </summary>
        public void SetupScheduler() {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            var sched = schedFact.GetScheduler();
            sched.Start();
            var job = new JobDetail("UpdateFiles", typeof(UpdateLocalFiles));
            job.JobDataMap.Add("Provider", this);
            var trigger = TriggerUtils.MakeHourlyTrigger(1);
            trigger.Name = "minuteTrigger";
            sched.ScheduleJob(job, trigger);
        }
        #endregion
    }
}