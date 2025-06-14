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

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Personalmedicine Personalmedicine { get; set; } = null!;
}
