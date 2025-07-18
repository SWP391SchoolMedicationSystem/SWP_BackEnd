using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.IService;
using BussinessLayer.Service;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Notifications;
using Microsoft.Extensions.Logging;
using Quartz;

namespace BussinessLayer.QuartzJob.Job
{
    public class MedicineNotifyJob(IEmailService emailService,
        ILogger<MedicineNotifyJob> logger,
        IParentService parentService,
        IScheduleDetailService scheduleDetailService,
        IPersonalmedicineService personalmedicineService,
        INotificationService notificationService
        ) : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            // This method will be called when the job is executed
            // You can implement your notification logic here

            var parentid = context.JobDetail.JobDataMap.Get("Parent");
            var detailid = context.JobDetail.JobDataMap.Get("Details");
            var personalmedicineid = context.JobDetail.JobDataMap.Get("PersonalMedicine");
            var note = context.JobDetail.JobDataMap.Get("Notes")?.ToString() ?? string.Empty;
            var duration = context.JobDetail.JobDataMap.GetInt("Duration");
            var parent = await parentService.GetParentByIdAsync((int)parentid);
            var details = await scheduleDetailService.GetScheduleDetailByIdAsync((int)detailid);
            var personalMedicine = await personalmedicineService.GetPersonalmedicineByIdAsync((int)personalmedicineid);
            string title = $"Medicine Reminder for {personalMedicine.MedicineName}";
            string body = $"Dear {parent.Fullname},\n\n" +
              $"This is a reminder for your child {parent.Students.FirstOrDefault()?.Fullname} to take the medicine {personalMedicine.MedicineName}.\n" +
              $"Details:\n" +
              $"Schedule: {details.Starttime}\n" +
              $"Notes: {note}\n" +
              "Please ensure that the medicine is taken as prescribed.\n\n";

            CreateNotificationDTO newnoti = new()
            {
                Title = title,
                CreatedAt = DateTime.Now,
                Type = "Personal Medicine",
                Message = body,
                Createdby = "Notification System",
                Createddate = DateTime.Now,
                IsDeleted = false
            }
                ;
            notificationService.CreateNotificationForStaff(newnoti);
            await Task.CompletedTask; // Simulate async work
        }
    }
}
