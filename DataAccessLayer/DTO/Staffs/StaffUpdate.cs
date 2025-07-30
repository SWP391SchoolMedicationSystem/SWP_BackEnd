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

        public string Fullname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;
        public int Roleid { get; set; }
    }
}
