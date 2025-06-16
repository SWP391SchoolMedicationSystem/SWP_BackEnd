using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MimeKit;

namespace BussinessLayer.Utils.Configurations
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; } = null!;
        public string Content { get; set; } = null!;
        public IFormFileCollection Attachments { get; set; } = null!;
        public Message(
        
            IEnumerable<string> to,
                string subject,
            string content,
            IFormFileCollection? attachments = null
        )
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(email => new MailboxAddress("recipent", email)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }

    }
}
