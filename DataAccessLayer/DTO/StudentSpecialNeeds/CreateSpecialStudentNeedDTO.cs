using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.DTO.StudentSpecialNeeds
{
    public class CreateSpecialStudentNeedDTO
    {
        public int StudentId { get; set; }

        public int SpecialNeedCategoryId { get; set; }

        public string? Notes { get; set; }
        public virtual SpecialNeedsCategory SpecialNeedCategory { get; set; } = null!;

        public virtual Student Student { get; set; } = null!;
    }
}
