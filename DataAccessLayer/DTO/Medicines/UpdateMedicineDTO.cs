using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Medicines
{
    public class UpdateMedicineDTO
    {


        public string MedicineName { get; set; } = null!;

        public int? MedicineCategoryId { get; set; }

        public string? Usage { get; set; }

        public string? DefaultDosage { get; set; }

        public string? SideEffects { get; set; }
        public bool IsDeleted { get; set; }
        public int? ModifiedByUserId { get; set; }

    }
}
