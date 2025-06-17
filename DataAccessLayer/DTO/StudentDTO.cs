using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.DTO
{
    public class StudentDTO
    {
        public string? StudentCode { get; set; }
        public string Fullname { get; set; } = null!;
        public string? BloodType { get; set; }

        public virtual Classroom Class { get; set; } = null!;
        public virtual Parent Parent { get; set; } = null!;
        public int Age { get; set; }

        public DateOnly Dob { get; set; }

        public bool Gender { get; set; }

    }
}
