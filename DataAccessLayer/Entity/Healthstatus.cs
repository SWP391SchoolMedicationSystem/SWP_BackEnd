using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Healthstatus
{
    public int HealthId { get; set; }

    public int StudentId { get; set; }

    public int? StaffId { get; set; }

    public int HealthStatusCategory { get; set; }

    public string? Description { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Healthstatuscategory HealthStatusCategoryNavigation { get; set; } = null!;

    public virtual Staff? Staff { get; set; }

    public virtual Student Student { get; set; } = null!;
}
