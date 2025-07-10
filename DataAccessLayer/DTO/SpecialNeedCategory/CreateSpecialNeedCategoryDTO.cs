using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.SpecialNeedCategory
{
    public class CreateSpecialNeedCategoryDTO
    {
        public string CategoryName { get; set; } = null!;

        public string? Description { get; set; }
        public int? CreatedByUserId { get; set; }

    }
}
