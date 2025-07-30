using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Form
{
    public class AddFormMedicine
    {
        [Required(ErrorMessage = "Vui lòng nhập ID phụ huynh")]
        [Range(1, int.MaxValue, ErrorMessage = "ID của phụ huynh phải lớn hơn 0")]

        public int ParentId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ID học sinh")]
        [Range(1, int.MaxValue, ErrorMessage = "ID của học sinh phải lớn hơn 0")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập tên thuốc")]
        public string MedicineName { get; set; } = null!;

        public string? MedicineDescription { get; set; } = null!;
        public string? Reason { get; set; } = null!;
        public IFormFile? DocumentFile { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên người tạo")]
        public string CreatedBy { get; set; } = null!;
    }
    public class AddFormAbsent
    {

        [Required(ErrorMessage = "Vui lòng nhập ID phụ huynh")]
        [Range(1, int.MaxValue, ErrorMessage = "ID của phụ huynh phải lớn hơn 0")]

        public int ParentId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ID học sinh")]
        [Range(1, int.MaxValue, ErrorMessage = "ID của học sinh phải lớn hơn 0")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập tên thuốc")]
        public string? ReasonForAbsent { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập ngày vắng mặt")]
        public string AbsentDate { get; set; } = null!;
        public IFormFile? DocumentFile { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên người tạo")]
        public string CreatedBy { get; set; } = null!;
    }
    public class AddFormChronicIllness
    {
        [Required(ErrorMessage = "Vui lòng nhập ID phụ huynh")]
        [Range(1, int.MaxValue, ErrorMessage = "ID của phụ huynh phải lớn hơn 0")]

        public int ParentId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ID học sinh")]
        [Range(1, int.MaxValue, ErrorMessage = "ID của học sinh phải lớn hơn 0")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập tên thuốc")]
        public string? ChronicIllnessName { get; set; } = null!;
        public string? ChronicIllnessDescription { get; set; } = null!;
        public string? Systoms { get; set; } = null!;
        public string? ActionRequired { get; set; } = null!;
        public IFormFile? DocumentFile { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên người tạo")]
        public string CreatedBy { get; set; } = null!;
    }
    public class AddFormPhysicalActivityModification
    {
        [Required(ErrorMessage = "Vui lòng nhập ID phụ huynh")]
        [Range(1, int.MaxValue, ErrorMessage = "ID của phụ huynh phải lớn hơn 0")]

        public int ParentId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ID học sinh")]
        [Range(1, int.MaxValue, ErrorMessage = "ID của học sinh phải lớn hơn 0")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập tên thuốc")]
        public string? ReasonForModification { get; set; } = null!;
        public string? ModificationDetails { get; set; } = null!;
        public IFormFile? DocumentFile { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên người tạo")]
        public string CreatedBy { get; set; } = null!;

    }

}
