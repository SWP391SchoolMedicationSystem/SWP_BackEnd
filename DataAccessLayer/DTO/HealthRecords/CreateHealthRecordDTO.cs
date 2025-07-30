using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.HealthRecords
{
    public class CreateHealthRecordDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập ID của học sinh")]
        [Range(1, int.MaxValue, ErrorMessage = "ID học sinh phải lớn hơn 0")]
        public int StudentID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ID của danh mục sức khỏe")]
        [Range(1, int.MaxValue, ErrorMessage = "ID danh mục sức khỏe phải lớn hơn 0")]
        public int HealthCategoryID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày ghi nhận sức khỏe")]
        [DataType(DataType.DateTime, ErrorMessage = "Ngày ghi nhận sức khỏe không hợp lệ")]
        public DateTime HealthRecordDate { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề ghi nhận sức khỏe")]
        public string Healthrecordtitle { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mô tả ghi nhận sức khỏe")]
        public string Healthrecorddescription { get; set; }
        public int Staffid { get; set; }
        public bool IsConfirm { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}
