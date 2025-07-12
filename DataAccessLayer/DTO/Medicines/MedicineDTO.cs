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


        public bool Isdeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedByUserId { get; set; }

        public string? CreatedByUserName { get; set; } = null!;

        public int? ModifiedByUserId { get; set; }

        public string? ModifiedByUserName { get; set; } = null!;
    }
}
