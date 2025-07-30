using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Parents
{
    public class ParentUpdate
    {
        [Required(ErrorMessage = "ParentID is required.")]

        public int Parentid { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên đầy đủ.")]
        public string? Fullname { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [RegularExpression("^0\\d{9,}$", ErrorMessage = "Vui lòng nhập số hợp lệ")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        public string? Address { get; set; } 

    }
}
