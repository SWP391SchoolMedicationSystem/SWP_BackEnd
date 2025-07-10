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

        public string? ApprovedByUserName { get; set; } = null!;

        public DateTime? ApprovedOn { get; set; }

        public DateTime? ResponseDate { get; set; }

        public string? ReasonForDecline { get; set; }

        public int CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; } = null!;

        public DateTime CreatedAt { get; set; }


        public int? ModifiedByUserId { get; set; }

        public string ModifiedByUserName { get; set; } = null!;

        public DateTime? ModifiedAt { get; set; }


        public string Status { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public string? Image { get; set; }
    }
}
