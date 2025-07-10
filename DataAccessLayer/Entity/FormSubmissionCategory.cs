using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class FormSubmissionCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();
}
