using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class PersonalMedicineScheduleDTO
    {
        public int Scheduledetails { get; set; }

        public int Personalmedicineid { get; set; }

        public string? Notes { get; set; }

        public int? Duration { get; set; }

        public DateTime? Startdate { get; set; }
        public bool Isdeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedByUserId { get; set; }

        public string? CreatedByUserName { get; set; } = null!;

        public int? ModifiedByUserId { get; set; }

        public string? ModifiedByUserName { get; set; } = null!;


    }
}
