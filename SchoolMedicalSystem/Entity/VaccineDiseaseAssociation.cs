using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class VaccineDiseaseAssociation
{
    public int AssociationId { get; set; }

    public int VaccineId { get; set; }

    public int DiseaseId { get; set; }

    public virtual Disease Disease { get; set; } = null!;

    public virtual Vaccine Vaccine { get; set; } = null!;
}
