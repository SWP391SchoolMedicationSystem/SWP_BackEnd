using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class StudentVaccinationRecord
{
    public int StudentVaccinationId { get; set; }

    public int StudentId { get; set; }

    public int VaccineId { get; set; }

    public int? EventId { get; set; }

    public string ParentalConsentStatus { get; set; } = null!;

    public DateTime? ConsentDate { get; set; }

    public DateTime? DateAdministered { get; set; }

    public int? AdministeredByStaffId { get; set; }

    public string? Notes { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Staff? AdministeredByStaff { get; set; }

    public virtual VaccinationEvent? Event { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual Vaccine Vaccine { get; set; } = null!;
}
