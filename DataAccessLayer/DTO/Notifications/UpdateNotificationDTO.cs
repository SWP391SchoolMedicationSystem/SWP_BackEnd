using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Notifications
{
    public class UpdateNotificationDTO
    {
        public string Title { get; set; } = null!;

        public string Type { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public string? Message { get; set; }

        public string? Modifiedby { get; set; }

        public DateTime? Modifieddate { get; set; }
    }
}
