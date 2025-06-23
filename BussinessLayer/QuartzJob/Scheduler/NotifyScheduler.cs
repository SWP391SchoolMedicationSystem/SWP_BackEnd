using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.QuartzJob.Job;
using DataAccessLayer.DTO;
using Quartz;

namespace BussinessLayer.QuartzJob.Scheduler
{
    public class NotifyScheduler

    {
        private readonly ISchedulerFactory _schedulerFactory;

        public NotifyScheduler(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }
        public async Task ScheduleNotifyJob(string notifyId, EmailDTO request, DateTime schedule)
        {
            if (request != null)
            {
                IScheduler _scheduler = await _schedulerFactory.GetScheduler();
                var job = JobBuilder.Create<NotifyJob>()
                    .WithIdentity($"reminderJob_{notifyId}")
                    .UsingJobData("To", request.To ?? string.Empty)
                    .UsingJobData("Subject", request.Subject ?? string.Empty)
                    .UsingJobData("Body", request.Body ?? string.Empty)
                    .Build();
                int time = (int)(schedule - DateTime.Now).TotalMinutes;
                var trigger = TriggerBuilder.Create()
                    .WithIdentity($"reminderTrigger_{notifyId}")
                    .StartAt(DateBuilder.FutureDate(time, IntervalUnit.Minute))
                    .Build();
                Console.WriteLine("Job scheduled: " + notifyId + ", run at" + DateBuilder.FutureDate(1, IntervalUnit.Minute) + ", at:" + DateTime.Now);

                await _scheduler.ScheduleJob(job, trigger);
            }
        }
    }
}
