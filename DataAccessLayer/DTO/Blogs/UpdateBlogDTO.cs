using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Blogs
{
    public class UpdateBlogDTO
    {
        public int BlogID { get; set; } 
        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public int? UpdatedBy { get; set; }

        public string Status { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public string? Image { get; set; }
    }
}
