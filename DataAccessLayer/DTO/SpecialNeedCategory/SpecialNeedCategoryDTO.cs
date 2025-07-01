using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.DTO.SpecialNeedCategory
{
    public class SpecialNeedCategoryDTO
    {
        public int SpecialNeedCategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? Description { get; set; }

        public virtual ICollection<StudentSpecialNeed> StudentSpecialNeeds { get; set; } = new List<StudentSpecialNeed>();
    }
}
