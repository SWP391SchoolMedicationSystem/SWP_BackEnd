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
    public class NotifyJob : IJob
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<NotifyJob> _logger;
        public NotifyJob(IEmailService emailService, ILogger<NotifyJob> logger) {
        _emailService = emailService;
            _logger = logger;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            // This method will be called when the job is executed
            // You can implement your notification logic here

            var to = context.JobDetail.JobDataMap.GetString("To");
            var subject = context.JobDetail.JobDataMap.GetString("Subject");
            var body = context.JobDetail.JobDataMap.GetString("Body");
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
