using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class Disease
{
    public int DiseaseId { get; set; }

    public string DiseaseName { get; set; } = null!;

    public string? Description { get; set; }

    public bool Isdeleted { get; set; }

    public virtual ICollection<VaccineDiseaseAssociation> VaccineDiseaseAssociations { get; set; } = new List<VaccineDiseaseAssociation>();
}
