using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class SpecialNeedsCategory
{
    public int SpecialNeedCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public virtual User? ModifiedByUser { get; set; }

    public virtual ICollection<StudentSpecialNeed> StudentSpecialNeeds { get; set; } = new List<StudentSpecialNeed>();
}
