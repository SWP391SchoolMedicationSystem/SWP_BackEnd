using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.IService;
using BussinessLayer.QuartzJob.Job;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using Quartz;

namespace BussinessLayer.QuartzJob.Scheduler
{
    public class NotifyScheduler

    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IScheduleDetailService _scheduleDetailService;
        private readonly IParentService _parentService;
        private readonly IPersonalmedicineService _personalMedicineService;

        public NotifyScheduler(ISchedulerFactory schedulerFactory, IParentService parentService, IScheduleDetailService scheduleDetailService
            , IPersonalmedicineService personalmedicineService)
        {
            _personalMedicineService = personalmedicineService;
            _parentService = parentService;
            _scheduleDetailService = scheduleDetailService;
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
        public async Task ScheduleMedicalNotifyJob(List<Medicineschedule> medicalSchedule)
        {
            if (medicalSchedule != null)
            {
                var Parent = await _parentService.GetAllParentsAsync();
                IScheduler _scheduler = await _schedulerFactory.GetScheduler();
                var personalmedicine = await _personalMedicineService.GetAllPersonalmedicinesAsync();
                var scheduledetail = await _scheduleDetailService.GetAllScheduleDetailsAsync();
                int rotation = 0;
                foreach (var medicalSchedules in medicalSchedule)
                {
                    var personalMedicine = personalmedicine.FirstOrDefault(p => p.Personalmedicineid == medicalSchedules.Personalmedicineid);
                    var parent = Parent.FirstOrDefault(p => p.Parentid == personalMedicine.Parentid);
                    var scheduleDetail = scheduledetail.FirstOrDefault(s => s.Scheduledetailid == medicalSchedules.Scheduledetails);
                    if (medicalSchedules.Startdate.Value.AddDays((double)(medicalSchedules.Duration * 7)) != DateTime.Now && scheduleDetail.Dayinweek == ((int)DateTime.Now.DayOfWeek) + 1 )
                    {
                        string notifyId = "medicalNotifyJob";
                        rotation++;
                        var job = JobBuilder.Create<MedicineNotifyJob>()
                        .WithIdentity($"medicalJob_{notifyId}")
                        .UsingJobData("Parent", parent.Parentid)
                        .UsingJobData("Details", medicalSchedules.Scheduledetails)
                        .UsingJobData("PersonalMedicine", medicalSchedules.Personalmedicineid)
                        .UsingJobData("Notes", medicalSchedules.Notes ?? string.Empty)
                        .UsingJobData("Duration", medicalSchedules.Duration ?? 0)
                        .Build();
                        var schedule = await _scheduleDetailService.GetScheduleDetailByIdAsync(medicalSchedules.Scheduledetails);

                        if (schedule.Starttime - TimeOnly.FromDateTime(DateTime.Now) < TimeSpan.Zero || schedule.Endtime - TimeOnly.FromDateTime(DateTime.Now) > TimeSpan.Zero)
                        {
                            var trigger = TriggerBuilder.Create()
                                .WithIdentity($"medicalTrigger_{notifyId}")
                                .StartAt(DateBuilder.FutureDate(10, IntervalUnit.Second))
                                .Build();
                            Console.WriteLine("Medical Job scheduled: " + medicalSchedules.Personalmedicineid + ", run at" + DateBuilder.FutureDate(10, IntervalUnit.Second) + ", at:" + DateTime.Now);
                            await _scheduler.ScheduleJob(job, trigger);

                        }
                        else
                        {
                            var time = schedule.Starttime - TimeOnly.FromDateTime(DateTime.Now);
                            var trigger = TriggerBuilder.Create()
                                .WithIdentity($"medicalTrigger_{notifyId}")
                                .StartAt(DateBuilder.FutureDate((time).Minutes, IntervalUnit.Minute))
                                .Build();
                            Console.WriteLine("Medical Job scheduled: " + medicalSchedules.Personalmedicineid + ", Note: " +medicalSchedules.Notes+ ", run at " + DateBuilder.FutureDate(time.Minutes, IntervalUnit.Minute) + ", at:" + DateTime.Now);
                            await _scheduler.ScheduleJob(job, trigger);
                        }
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
