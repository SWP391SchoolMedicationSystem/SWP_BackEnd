using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.IService;
using BussinessLayer.QuartzJob.Job;
using DataAccessLayer.DTO;
using Quartz;

namespace BussinessLayer.QuartzJob.Scheduler
{
    public class NotifyScheduler

    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IScheduleDetailService _scheduleDetailService;

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
        public async Task ScheduleMedicalNotifyJob(string notifyId, List<MedicalScheduleDTO> medicalSchedule)
        {
            if (medicalSchedule != null)
            {

                IScheduler _scheduler = await _schedulerFactory.GetScheduler();
                foreach (var medicalSchedules in medicalSchedule)
                {
                    if (medicalSchedules.Startdate.Value.AddDays((double)(medicalSchedules.Duration * 7)) != DateTime.Now)
                    {
                        var job = JobBuilder.Create<MedicineNotifyJob>()
                        .WithIdentity($"medicalJob_{notifyId}")
                        .UsingJobData("Details", medicalSchedules.Scheduledetails)
                        .UsingJobData("PersonalMedicine", medicalSchedules.Personalmedicineid)
                        .UsingJobData("Notes", medicalSchedules.Notes ?? string.Empty)
                        .UsingJobData("Duration", medicalSchedules.Duration ?? 0)
                        .Build();
                        var schedule = _scheduleDetailService.GetScheduleDetailByIdAsync(medicalSchedules.Scheduledetails);
                        var time = (schedule.Result.Starttime - TimeOnly.FromDateTime(DateTime.Now));
                        var trigger = TriggerBuilder.Create()
                            .WithIdentity($"medicalTrigger_{notifyId}")
                            .StartAt(DateBuilder.FutureDate(time.Minutes, IntervalUnit.Minute))
                            .Build();
                        Console.WriteLine("Medical Job scheduled: " + medicalSchedules.Personalmedicineid + ", run at" + DateBuilder.FutureDate(1, IntervalUnit.Minute) + ", at:" + DateTime.Now);
                        await _scheduler.ScheduleJob(job, trigger);
                    }
                    else
                    {
                        Console.WriteLine("Medical Job not scheduled: " + medicalSchedules.Personalmedicineid + ", at:" + DateTime.Now);
                    }
                }
            }
        }
    }
}
