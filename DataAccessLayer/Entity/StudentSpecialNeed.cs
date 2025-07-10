using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class StudentSpecialNeed
{
    public int StudentSpecialNeedId { get; set; }

    public int StudentId { get; set; }

    public int SpecialNeedCategoryId { get; set; }

    public string? Notes { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    public virtual SpecialNeedsCategory SpecialNeedCategory { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
