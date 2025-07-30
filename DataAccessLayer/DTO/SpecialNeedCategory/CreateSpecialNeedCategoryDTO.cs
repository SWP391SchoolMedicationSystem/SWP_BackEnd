using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.SpecialNeedCategory
{
    public class CreateSpecialNeedCategoryDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập tên danh mục chăm sóc đặc biệt")]
        public string CategoryName { get; set; } = null!;

        public string? Description { get; set; }
    }
}
