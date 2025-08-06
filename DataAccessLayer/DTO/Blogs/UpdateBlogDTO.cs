using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DataAccessLayer.DTO.Blogs
{
    public class UpdateBlogDTO
    {
        public int BlogID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string Content { get; set; } = null!;
        public int? UpdatedBy { get; set; }

        public string Status { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
