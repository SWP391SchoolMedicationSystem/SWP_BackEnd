using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Notifications
{
    public class CreateNotificationDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập ngày tạo")]
        public DateTime CreatedAt { get; set; }

        public string Type { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public string? Message { get; set; }

        public string? Createdby { get; set; }

        public DateTime? Createddate { get; set; }
    }
}
