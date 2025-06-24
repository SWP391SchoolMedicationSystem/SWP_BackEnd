using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.HealthStatus
{
    public class HealthStatusCategoryDTO
    {
        public string HealthStatusCategoryName { get; set; } = null!;

        public string? HealthStatusCategoryDescription { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
