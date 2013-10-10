using System.Collections.Generic;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace Dropbox {
    public class DropboxSync {
        public void SetupScheduler() {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();
            var dic = new Dictionary<string, object> {{"httpContext", HttpContext.Current}};
            var jobData = new JobDataMap((IDictionary<string, object>) dic);
            IJobDetail jobDetail = JobBuilder.Create<UpdateRavenJob>()
                .WithIdentity("updateAssets")
                .SetJobData(jobData)
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger")
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
                .StartAt(DateBuilder.FutureDate(1, IntervalUnit.Second))
                .Build();

            sched.ScheduleJob(jobDetail, trigger);
        }
    }
}
