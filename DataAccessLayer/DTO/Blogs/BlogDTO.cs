using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Blogs
{
    public class BlogDTO
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public int? ApprovedBy { get; set; }
        public string ApprovedByName { get; set; } = null!;

        public DateTime? ApprovedOn { get; set; }

        public int? CreatedBy { get; set; }
        public string CreatedByName { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }
        public string UpdatedByName { get; set; } = null!;

        public DateTime? UpdatedAt { get; set; }

        public string Status { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public string? Image { get; set; } = null;
    }
}
