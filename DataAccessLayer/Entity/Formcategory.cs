using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class FormCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<MedicalForm> MedicalForms { get; set; } = new List<MedicalForm>();
}
