using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.MedicineCategory
{
    public class UpdateMedicineCategoryDTO
    {
        public int Medicinecategoryid { get; set; }

        public string Medicinecategoryname { get; set; } = null!;

        public string? Description { get; set; }
    }
}
