using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class HealthCategoryDTO
    {
        public string Healthcategoryname { get; set; } = null!;

        public string? Healthcategorydescription { get; set; }
    }
}
