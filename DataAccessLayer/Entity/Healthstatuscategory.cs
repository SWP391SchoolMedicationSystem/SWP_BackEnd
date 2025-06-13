using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Healthstatuscategory
{
    public int HealthStatusCategoryId { get; set; }

    public string HealthStatusCategoryName { get; set; } = null!;

    public string? HealthStatusCategoryDescription { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Healthstatus> Healthstatuses { get; set; } = new List<Healthstatus>();
}
