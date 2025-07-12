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


        public string Type { get; set; } = null!;
        public string? Message { get; set; }


        public bool Isdeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedByUserId { get; set; }

        public string? CreatedByUserName { get; set; } = null!;

        public int? ModifiedByUserId { get; set; }

        public string? ModifiedByUserName { get; set; } = null!;


    }
}
