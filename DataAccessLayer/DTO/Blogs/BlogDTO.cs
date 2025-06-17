using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Blogs
{
    public class BlogDTO
    {
        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string Status { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public string? Image { get; set; }
    }
}
