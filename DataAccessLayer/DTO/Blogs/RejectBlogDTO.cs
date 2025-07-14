using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Blogs
{
    public class RejectBlogDTO
    {
        public int BlogId { get; set; }
        public int? ApprovedBy { get; set; }
        public string Message { get; set; } = null!; // Reason for rejection
    }
}
