using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class PersonalMedicineScheduleDTO
    {
        public DateTime Scheduletime { get; set; }

        public string Dose { get; set; } = null!;

        public string? Reason { get; set; }

        public bool Isconfirm { get; set; }
        public bool IsDeleted { get; set; }

    }
}
