using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class NotificationDTO
    {
        public string Title { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string Type { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public string? Createdby { get; set; }

        public DateTime? Createddate { get; set; }

        public string? Modifiedby { get; set; }

        public DateTime? Modifieddate { get; set; }
        public List<int> ParentIds { get; set; } = new();
    }
}
