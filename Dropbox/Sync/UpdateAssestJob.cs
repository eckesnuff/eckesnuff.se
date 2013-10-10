using System.IO;
using System.Web;
using Dropbox.Hosting;
using Quartz;

namespace Dropbox.Sync {
    [DisallowConcurrentExecution]
    class UpdateAssestJob : IJob {
        private readonly DropboxVirtualPathProvider _provider;

        public UpdateAssestJob() {
            _provider = new DropboxVirtualPathProvider();
        }

        string LoadDeltaCursor() {
            var path = HttpContext.Current.Server.MapPath("~/App_Data/DeltaCursor.cur");
            if(File.Exists(path))
                return File.ReadAllText(path);
            return string.Empty;
        }
        void SetDeltaCursor(string deltaCursor) {
            var path = HttpContext.Current.Server.MapPath("~/App_Data/DeltaCursor.cur");
            File.WriteAllText(path,deltaCursor);
        }

        FileAction GetChanges() {
            var changes = _provider.DropBoxClient.GetDelta(LoadDeltaCursor());
            var fileAction = new FileAction {DeltaCursor = changes.Cursor};
            if(changes.Entries.Count>0) {
                foreach (var deltaEntry in changes.Entries) {
                    if(deltaEntry.MetaData==null) {
                        fileAction.PathsForDeletion.Add(deltaEntry.Path);
                    }
                    else if (!deltaEntry.MetaData.Is_Dir && deltaEntry.MetaData.Bytes > 0) {
                        fileAction.FilesForCreationOrUpdate.Add(deltaEntry);
                    }
                }
            }
            return fileAction;
        }

        public void Execute(IJobExecutionContext context) {
            HttpContext.Current = context.JobDetail.JobDataMap["httpContext"] as HttpContext;
            var settings = context.JobDetail.JobDataMap["Settings"] as Settings;
            var changes = GetChanges();
            new Worker(settings).Execute(changes);
            SetDeltaCursor(changes.DeltaCursor);
        }
    }
}