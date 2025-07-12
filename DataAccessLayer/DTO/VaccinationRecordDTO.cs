using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class VaccinationRecordDTO
    {
        public int StudentVaccinationId { get; set; }

        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public int VaccineId { get; set; }
        public string VaccineName { get; set; } 


        public int? EventId { get; set; }

        public string? EventName { get; set; }
        public int dosenumber { get; set; }

        public string ParentalConsentStatus { get; set; } = null!;

        public string? ReasonForDecline { get; set; }

        public DateTime? ConsentResponseDate { get; set; }

        public DateTime? ConsentDate { get; set; }

        public DateTime? DateAdministered { get; set; }

        public int? AdministeredByStaffId { get; set; }

        public string? Notes { get; set; }

        public bool Isdeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedByUserId { get; set; }

        public string? CreatedByUserName { get; set; } = null!;

        public int? ModifiedByUserId { get; set; }

        public string? ModifiedByUserName { get; set; } = null!;

    }
}
