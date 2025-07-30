using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.DTO.StudentSpecialNeeds
{
    public class CreateSpecialStudentNeedDTO
    {
        [Required(ErrorMessage = "Vui lòng chọn học sinh cần chăm sóc đặc biệt")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn vấn đề cần chú ý")]
        public int SpecialNeedCategoryId { get; set; }

        public string? Notes { get; set; }
 //       public virtual SpecialNeedsCategory SpecialNeedCategory { get; set; } = null!;

//        public virtual Student Student { get; set; } = null!;
    }
}
