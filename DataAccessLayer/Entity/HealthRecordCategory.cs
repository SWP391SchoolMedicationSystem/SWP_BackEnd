using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class HealthRecordCategory
{
    public int HealthCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? CategoryDescription { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public virtual ICollection<HealthRecord> HealthRecords { get; set; } = new List<HealthRecord>();

    public virtual User? ModifiedByUser { get; set; }
}
