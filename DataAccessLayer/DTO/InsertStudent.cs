using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class InsertStudent
    {
        [Required(ErrorMessage = "Mã học sinh là bắt buộc")]
        public string studentCode { get; set; } = null!;
        [Required(ErrorMessage = "Tên học sinh là bắt buộc")]
        public string fullName { get; set; } = null!;
        [Required(ErrorMessage = "Nhóm máu là bắt buộc")]
        public string bloodtype { get; set; } = null!;
        [Required(ErrorMessage = "Lớp học là bắt buộc")]
        public string className { get; set; } = null!;
        [Required(ErrorMessage = "Tên phụ huynh là bắt buộc")]
        public string parentName { get; set; } = null!;
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        public string parentphone { get; set; } = null!;
        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        [DataType(DataType.Date, ErrorMessage = "Ngày sinh không hợp lệ")]
        public DateOnly birthDate { get; set; }
        [Required(ErrorMessage = "Giới tính là bắt buộc")]
        public string gender { get; set; } = null!;
        [Required(ErrorMessage = "Tình trạng sức khỏe")]
        public string healthStatus { get; set; } = null!;

    }
}
