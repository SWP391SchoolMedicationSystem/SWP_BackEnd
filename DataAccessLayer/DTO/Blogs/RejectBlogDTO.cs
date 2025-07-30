using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Blogs
{
    public class RejectBlogDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập ID của Blog")]
        [Range(1, int.MaxValue, ErrorMessage = "ID của Blog phải lớn hơn 0")]
        public int BlogId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ID người duyệt")]
        public int? ApprovedBy { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập lý do từ chối")]
        public string Message { get; set; } = null!; // Reason for rejection
    }
}
