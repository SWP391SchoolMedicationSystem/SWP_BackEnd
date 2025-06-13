using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Personalmedicineschedule
{
    public int Personalmedicineid { get; set; }

    public DateTime Scheduletime { get; set; }

    public string Dose { get; set; } = null!;

    public string? Reason { get; set; }

    public bool Isconfirm { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Personalmedicine Personalmedicine { get; set; } = null!;
}
