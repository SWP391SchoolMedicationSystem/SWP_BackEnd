using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DataAccessLayer.DTO.Blogs
{
    public class CreateBlogDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string Content { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập Id người tạo")]
        public int CreatedBy { get; set; } //FE return 

        public IFormFile? ImageFile { get; set; }
    }
}
