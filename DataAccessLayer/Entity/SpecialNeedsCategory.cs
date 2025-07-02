using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class SpecialNeedsCategory
{
    public int SpecialNeedCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }
    public bool IsDelete { get; set; }

    public virtual ICollection<StudentSpecialNeed> StudentSpecialNeeds { get; set; } = new List<StudentSpecialNeed>();
}
