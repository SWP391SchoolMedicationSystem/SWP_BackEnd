using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Medicines
{
    public class CreateMedicineDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập tên thuốc")]
        public string Medicinename { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng chọn danh mục thuốc")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục thuốc hợp lệ")]
        public int Medicinecategoryid { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập loại thuốc")]

        public string Type { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập số lượng thuốc")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng thuốc phải lớn hơn 0")]
        public int Quantity { get; set; }

        public DateTime? Createdat { get; set; }
        public string? Createdby { get; set; }
    }
}
