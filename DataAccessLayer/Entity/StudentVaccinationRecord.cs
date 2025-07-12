using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class StudentVaccinationRecord
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

    public virtual Staff? AdministeredByStaff { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public virtual VaccinationEvent? Event { get; set; }

    public virtual User? ModifiedByUser { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual Vaccine Vaccine { get; set; } = null!;
}
