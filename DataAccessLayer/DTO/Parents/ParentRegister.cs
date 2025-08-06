using DataAccessLayer.DTO.Students;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Parents
{
    public class ParentRegister
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đầy đủ.")]
        public string Fullname { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [RegularExpression("^0\\d{9,}$", ErrorMessage = "Vui lòng nhập số hợp lệ")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [RegularExpression(@"^(?=.{8,})(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*]).*$",
            ErrorMessage = "Mật khẩu cần ít nhất 8 chữ cái," +
            "bao gồm 1 chữ in hoa, 1 số và 1 ký tự đặc biệt ")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]

        public string Address { get; set; } = null!;

        public List<AddStudentDTO> Students { get; set; } = [];
    }
}
