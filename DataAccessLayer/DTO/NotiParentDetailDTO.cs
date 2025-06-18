using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.DTO
{
    public class NotiParentDetailDTO
    {
        public int NotificationId { get; set; }

        public int ParentId { get; set; }

        public string? Message { get; set; }

        public bool IsRead { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public virtual Notification Notification { get; set; } = null!;

        public virtual Parent Parent { get; set; } = null!;
    }
}
