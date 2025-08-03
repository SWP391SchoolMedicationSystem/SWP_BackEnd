using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Students
{
    public class AddStudentDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập số báo danh")]
        [StringLength(10, ErrorMessage = "Số báo danh không được vượt quá 10 ký tự")]
        public string? StudentCode { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string Fullname { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập tuổi")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập nhóm máu")]
        public string? BloodType { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public bool Gender { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn lớp học")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn lớp học hợp lệ")]
        public int Classid { get; set; } = 0;
        [Required(ErrorMessage = "Vui lòng chọn phụ huynh của học sinh")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn phụ huynh hợp lệ")]
        public int Parentid { get; set; } = 0;
        [Required(ErrorMessage = "Vui lòng nhập ngày sinh")]
        [DataType(DataType.Date, ErrorMessage = "Vui lòng nhập ngày sinh hợp lệ")]
        public DateOnly Dob { get; set; }
    }
}
