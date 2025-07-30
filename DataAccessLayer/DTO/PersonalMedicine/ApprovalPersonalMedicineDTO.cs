using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.PersonalMedicine
{
    public class ApprovalPersonalMedicineDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập thuốc cá nhân")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn thuốc cá nhân hợp lí")]
        public int Personalmedicineid { get; set; }

        public string? Approvedby { get; set; }


    }
}
