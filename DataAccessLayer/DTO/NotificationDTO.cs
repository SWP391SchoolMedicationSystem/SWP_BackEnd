using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class NotificationDTO
    {
        public string Title { get; set; }
        public DateOnly CreatedAt { get; set; }
        public string Type { get; set; }
        public bool isDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateOnly CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateOnly ModifiedDate { get; set; }
        public List<int> ParentIds { get; set; } = new();
    }
}
