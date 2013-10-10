using System.Collections.Generic;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace Dropbox.Sync {
    public class DropboxSync {
        public void SetupScheduler(Settings settings) {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();
            var dic = new Dictionary<string, object> {{"httpContext", HttpContext.Current}, {"Settings", settings}};
            var jobData = new JobDataMap((IDictionary<string, object>) dic);
            IJobDetail jobDetail = JobBuilder.Create<UpdateAssestJob>()
                .WithIdentity("updateAssets")
                .SetJobData(jobData)
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger")
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(settings.IntervalInSeconds).RepeatForever())
                .StartAt(DateBuilder.FutureDate(5, IntervalUnit.Second))
                .Build();

            sched.ScheduleJob(jobDetail, trigger);
        }
    }
}
