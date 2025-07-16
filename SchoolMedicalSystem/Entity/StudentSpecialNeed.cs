using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class StudentSpecialNeed
{
    public int StudentSpecialNeedId { get; set; }

    public int StudentId { get; set; }

    public int SpecialNeedCategoryId { get; set; }

    public string? Notes { get; set; }

    public bool Isdeleted { get; set; }

    public virtual SpecialNeedsCategory SpecialNeedCategory { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
