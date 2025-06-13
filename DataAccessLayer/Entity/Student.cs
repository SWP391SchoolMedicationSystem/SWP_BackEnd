using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Student
{
    public int Studentid { get; set; }

    public string Fullname { get; set; } = null!;

    public int Age { get; set; }

    public DateOnly Dob { get; set; }

    public bool Gender { get; set; }

    public int Classid { get; set; }

    public int Parentid { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<Healthrecord> Healthrecords { get; set; } = new List<Healthrecord>();

    public virtual ICollection<Healthstatus> Healthstatuses { get; set; } = new List<Healthstatus>();

    public virtual Parent Parent { get; set; } = null!;

    public virtual ICollection<Personalmedicine> Personalmedicines { get; set; } = new List<Personalmedicine>();
}
