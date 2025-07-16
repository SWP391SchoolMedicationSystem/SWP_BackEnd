using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using Microsoft.Extensions.Logging;
using Quartz;

namespace BussinessLayer.QuartzJob.Job
{
    public class MedicineNotifyJob : IJob
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<MedicineNotifyJob> _logger;
        public MedicineNotifyJob(IEmailService emailService, ILogger<MedicineNotifyJob> logger) {
        _emailService = emailService;
            _logger = logger;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            // This method will be called when the job is executed
            // You can implement your notification logic here

            var to = context.JobDetail.JobDataMap.Get("Details");
            var subject = context.JobDetail.JobDataMap.Get("PersonalMedicine");
            var body = context.JobDetail.JobDataMap.Get("Notes")?.ToString() ?? string.Empty;
            var duration = context.JobDetail.JobDataMap.GetInt("Duration");
            EmailDTO request = new()
            {
                To = to,
                Subject = subject,
                Body = body
            };
            await _emailService.SendEmailAsync(request);
            await Task.CompletedTask; // Simulate async work
        }
    }
}
