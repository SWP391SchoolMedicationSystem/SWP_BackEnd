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

    }
}
