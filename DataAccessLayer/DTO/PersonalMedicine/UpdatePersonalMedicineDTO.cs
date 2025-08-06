using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.PersonalMedicine
{
    public class UpdatePersonalMedicineDTO
    {
        public int personalMedicineId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thuốc")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn thuốc hợp lí")]
        public int Medicineid { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn phụ huynh")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn phụ huynh hợp lí")]
        public int? Parentid { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn học sinh")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn học sinh hợp lí")]
        public int? Studentid { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập người tạo")]
        public string Createdby { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập ngày nhận thuốc")]
        [DataType(DataType.DateTime, ErrorMessage = "Ngày ghi nhận sức khỏe không hợp lệ")]

        public DateTime Receiveddate { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "Ngày hết hạn không hợp lệ")]
        public DateTime? ExpiryDate { get; set; }
        public bool Status { get; set; }
        public string? Note { get; set; }
    }
}
