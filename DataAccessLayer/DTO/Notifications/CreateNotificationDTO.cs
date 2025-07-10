using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Notifications
{
    public class CreateNotificationDTO
    {
        public string Title { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string? Message { get; set; }

        public int? CreatedByUserId { get; set; }

    }
}
