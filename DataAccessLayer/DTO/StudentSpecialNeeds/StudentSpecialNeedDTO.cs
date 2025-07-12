using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.DTO.StudentSpecialNeeds
{
    public class StudentSpecialNeedDTO
    {
        public int StudentSpecialNeedId { get; set; }

        public int StudentId { get; set; }

        public int SpecialNeedCategoryId { get; set; }

        public string? Notes { get; set; }
        public bool Isdeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedByUserId { get; set; }

        public string? CreatedByUserName { get; set; } = null!;

        public int? ModifiedByUserId { get; set; }

        public string? ModifiedByUserName { get; set; } = null!;

        public virtual SpecialNeedsCategory SpecialNeedCategory { get; set; } = null!;

        public virtual Student Student { get; set; } = null!;
    }
}
