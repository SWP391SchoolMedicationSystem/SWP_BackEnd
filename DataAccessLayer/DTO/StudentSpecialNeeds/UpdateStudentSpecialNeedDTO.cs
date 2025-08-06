using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.DTO.StudentSpecialNeeds
{
    public class UpdateStudentSpecialNeedDTO
    {
        public int StudentSpecialNeedId { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn học sinh cần chăm sóc đặc biệt")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn vấn đề cần chú ý")]
        public int SpecialNeedCategoryId { get; set; }

        public string? Notes { get; set; }
        public bool IsDelete { get; set; }
    }
}
