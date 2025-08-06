using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Staffs
{
    public class StaffUpdate
    {
        [Required(ErrorMessage = "Vui lòng chọn nhân viên")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn nhân viên hợp lệ")]
        public int Staffid { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên đầy đủ.")]
        public string Fullname { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]

        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [RegularExpression("^0\\d{9,}$", ErrorMessage = "Vui lòng nhập số hợp lệ")]

        public string Phone { get; set; } = null!;
        public int Roleid { get; set; }
    }
}
