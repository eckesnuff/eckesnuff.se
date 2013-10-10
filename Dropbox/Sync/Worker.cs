using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using BrickPile.Core.Infrastructure.Indexes;
using BrickPile.Domain.Models;
using BrickPile.UI.Web;
using Common.Logging;
using DropNet.Models;
using Dropbox.Hosting;
using Raven.Client;

namespace Dropbox.Sync {
    public class Worker {
        private readonly DropboxVirtualPathProvider _vpp;
        private readonly IDocumentStore _store;
        private readonly ILog _logger;
        private Settings _settings;

        public Worker(Settings settings) {
            _vpp = new DropboxVirtualPathProvider();
            _store = StructureMap.ObjectFactory.GetInstance<IDocumentStore>();
            _logger = LogManager.GetCurrentClassLogger();
            _settings = settings;
        }

        public void Execute(FileAction action) {
            Delete(action.PathsForDeletion);
            UploadFiles(action.FilesForCreationOrUpdate);
        }
        private Asset GetAsset(IDocumentSession session, DeltaEntry entry) {
            var path = GetPath(entry.Path);
            var asset =
                session.Query<Asset, AllAssets>().Customize(x => x.WaitForNonStaleResultsAsOfLastWrite()).FirstOrDefault(
                    x => x.VirtualPath == path);
            return asset;
        }

        private void UploadFiles(List<DeltaEntry> filesForCreationOrUpdate) {
            if(filesForCreationOrUpdate.Count==0) {
                return;
            }
            using (var session = _store.OpenSession()) {
                foreach (var deltaEntry in filesForCreationOrUpdate) {
                    try {
                        Asset file = GetAsset(session, deltaEntry);
                        var id = file != null ? file.Id : null;
                        var assetChangeDate = file != null ? file.DateUploaded : DateTime.MinValue;
                        var diff = assetChangeDate - deltaEntry.MetaData.UTCDateModified.ToLocalTime();
                        //TODO remove ToLocal when date i saved in utc
                        if (diff > new TimeSpan(0) && diff < TimeSpan.FromSeconds(_settings.IntervalInSeconds))
                        {
                            //Skip file if it was uploaded to UI after previous run
                            continue;
                        }
                        var virtualFile = (DropboxVirtualFile) _vpp.GetFile(GetPath(deltaEntry.Path));
                        Stream stream = virtualFile.Open();
                        if (virtualFile.MetaData.Thumb_Exists) {
                            file = file ?? new Image();
                            using (var image = System.Drawing.Image.FromStream(stream, false, false)) {
                                ((Image) file).Width = image.Width;
                                ((Image) file).Height = image.Height;
                            }
                            var mediumThumbnail = new WebImage(stream).Resize(111, 101).Crop(1, 1);
                            file.Thumbnail = mediumThumbnail.GetBytes();
                        }
                        else {
                            var icon =
                                new WebImage(HttpContext.Current.Server.MapPath(_settings.DocumentIcon));
                            file = file ?? new Video {Thumbnail = icon.GetBytes()};
                        }

                        file.Name = virtualFile.MetaData.Name;
                        file.ContentType = MimeMapping.GetMimeMapping(virtualFile.MetaData.Name);
                        file.ContentLength = virtualFile.MetaData.Bytes;
                        file.DateUploaded = virtualFile.MetaData.UTCDateModified;
                        file.VirtualPath = virtualFile.VirtualPath;
                        file.Url = virtualFile.Url;
                        if(id==null)
                            session.Store(file);
                        
                        session.SaveChanges();
                    }
                    catch (Exception ex) {
                        LogError("Some files: could not be updated or added", ex,
                                 string.Join(", ", filesForCreationOrUpdate));
                    }
                }

            }

        }

        void Delete(List<string> paths) {
            try {
                if(paths.Count==0) {
                    return;
                }
                using (var session = _store.OpenSession()) {
                    foreach (var path in paths) {
                        var currentPath = GetPath(path);
                        var asset = session.Query<Asset, AllAssets>().FirstOrDefault(x => x.VirtualPath == currentPath);
                        if (asset != null) {
                            session.Delete(asset);
                        }
                    }
                    session.SaveChanges();
                }
            }
            catch (Exception ex) {
                LogError("some files: {0} could not be deleted", ex, string.Join(", ", paths));
            }
        }

        private string GetPath(string path) {
            return VirtualPathUtility.RemoveTrailingSlash(VirtualPathUtility.ToAbsolute(_vpp.VirtualPathRoot)) + path;
        }

        private void LogError(string format, Exception ex,params object[] args) {
            _logger.ErrorFormat(format, ex, args);
        }
    }
}