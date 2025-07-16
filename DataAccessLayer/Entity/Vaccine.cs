using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Vaccine
{
    public int VaccineId { get; set; }

    public string VaccineName { get; set; } = null!;

    public string? BrandName { get; set; }

    public string? Description { get; set; }

    public bool Isdeleted { get; set; }

    public virtual ICollection<VaccineDiseaseAssociation> VaccineDiseaseAssociations { get; set; } = new List<VaccineDiseaseAssociation>();
}
