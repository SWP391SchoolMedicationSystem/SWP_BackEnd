using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Notifications
{
    public class NotificationDTO
    {
        public int NotificationId { get; set; }

        public string Title { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string Type { get; set; } = null!;
        public string? Message { get; set; }


        public bool IsDeleted { get; set; }

        public int? CreatedByUserId { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? ModifiedByUserId { get; set; }


    }
}
