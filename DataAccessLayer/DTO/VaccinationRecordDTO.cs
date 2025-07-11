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

        public int VaccineId { get; set; }

        public int? EventId { get; set; }

        public int DoseNumber { get; set; }

        public string ParentalConsentStatus { get; set; } = null!;

        public string? ReasonForDecline { get; set; }

        public DateTime? ConsentResponseDate { get; set; }

        public DateTime? ConsentDate { get; set; }

        public DateTime? DateAdministered { get; set; }

        public int? AdministeredByStaffId { get; set; }

        public string? Notes { get; set; }

        public bool IsDeleted { get; set; }

        public int? CreatedByUserId { get; set; }

        public int? ModifiedByUserId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

    }
}
