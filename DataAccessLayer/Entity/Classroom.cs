using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entity;

public partial class Classroom
{
    public int Classid { get; set; }

    public string Grade { get; set; } = null!;

    public string Classname { get; set; } = null!;

    public string Teachername { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
