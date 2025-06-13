using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Staff
{
    public int Staffid { get; set; }

    public string Fullname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Phone { get; set; }

    public int Roleid { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public int Userid { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Healthrecord> HealthrecordCreatedbyNavigations { get; set; } = new List<Healthrecord>();

    public virtual ICollection<Healthrecord> HealthrecordModifiedbyNavigations { get; set; } = new List<Healthrecord>();

    public virtual ICollection<Healthrecord> HealthrecordStaffs { get; set; } = new List<Healthrecord>();

    public virtual ICollection<Healthstatus> Healthstatuses { get; set; } = new List<Healthstatus>();

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual ICollection<Personalmedicine> Personalmedicines { get; set; } = new List<Personalmedicine>();

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
