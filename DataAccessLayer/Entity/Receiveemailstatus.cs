using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class Receiveemailstatus
    {
        public int Id { get; set; }
        public int EmailTemplateId { get; set; }
        public string Email { get; set; } = null!;
        public string Status { get; set; }
        public DateTime SendDate { get; set; }
    }
}
