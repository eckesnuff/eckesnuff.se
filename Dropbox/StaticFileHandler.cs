using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Hosting;
using Dropbox.Hosting;

namespace Dropbox {
    public class StaticFileHandler : IHttpHandler {
        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.</returns>
        public bool IsReusable {
            get { return true; }
        }

        public int BypassFileSize {
            get { return int.Parse(ConfigurationManager.AppSettings["DropboxBypassFileSize"] ?? "0"); }
        }
        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public void ProcessRequest(HttpContext context) {

            var virtualFile =
                HostingEnvironment.VirtualPathProvider.GetFile(context.Request.FilePath) as DropboxVirtualFile;
            if (virtualFile==null|| !virtualFile.Exists) {
                throw new HttpException(404, "File not found");
            }
            if (virtualFile.MetaData.Bytes > 1024 * BypassFileSize) {
                var share = virtualFile.GetSharedUrl();
                context.Response.Redirect("http://dl.dropboxusercontent.com" + share.PathAndQuery, true);
            }
            var lastWriteTime = virtualFile.MetaData.ModifiedDate;
            var lastModified = new DateTime(lastWriteTime.Year, lastWriteTime.Month, lastWriteTime.Day,
                                            lastWriteTime.Hour, lastWriteTime.Minute, lastWriteTime.Second, 0);

            DateTime now = DateTime.Now;
            if (lastModified > now) {
                lastModified = new DateTime(now.Ticks - (now.Ticks%0x989680L));
            }

            var etag = GenerateETag(lastModified, now);

            SetCacheParamters(context, MimeMapping.GetMimeMapping(virtualFile.MetaData.Path),
                              virtualFile.MetaData.Path, lastModified, etag);

            using (Stream stream = virtualFile.Open()) {
                stream.CopyTo(context.Response.OutputStream);
            }


        }

        /// <summary>
        /// Sets the cache paramters.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <param name="localPath">The local path.</param>
        /// <param name="lastModified">The last modified.</param>
        /// <param name="etag">The etag.</param>
        private void SetCacheParamters(HttpContext context, string mimeType, string localPath, DateTime lastModified, string etag) {
            context.Response.ContentType = mimeType;
            context.Response.Cache.SetExpires(DateTime.Now.AddDays(3.0));
            context.Response.Cache.SetLastModified(lastModified);
            context.Response.Cache.SetETag(etag);
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
        }
        private static string GenerateETag(DateTime lastModified, DateTime now) {
            long num = lastModified.ToFileTime();
            long num2 = now.ToFileTime();
            string str = num.ToString("X8", CultureInfo.InvariantCulture);
            if ((num2 - num) <= 0x1c9c380L) {
                return ("W/\"" + str + "\"");
            }
            return ("\"" + str + "\"");
        }
    }
}
