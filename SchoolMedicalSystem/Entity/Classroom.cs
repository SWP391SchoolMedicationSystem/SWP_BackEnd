using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class Classroom
{
    public int Classid { get; set; }

    public int Grade { get; set; }

    public string Classname { get; set; } = null!;

    public string Teachername { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createddate { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifieddate { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
