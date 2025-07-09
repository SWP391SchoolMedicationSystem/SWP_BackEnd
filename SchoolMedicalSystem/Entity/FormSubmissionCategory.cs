using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class FormSubmissionCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();
}
