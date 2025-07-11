using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Medicines
{
    public class MedicineDTO
    {
        public int MedicineId { get; set; }

        public string MedicineName { get; set; } = null!;

        public int? MedicineCategoryId { get; set; }

        public string MedicineCategoryName { get; set; } = null!;

        public string? Usage { get; set; }

        public string? DefaultDosage { get; set; }

        public string? SideEffects { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? createdat { get; set; }

        public DateTime? updatedat { get; set; }

        public int? createdby { get; set; }

        public int? updatedby { get; set; }
    }
}
