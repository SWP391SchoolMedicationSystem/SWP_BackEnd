using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Students
{
    public class StudentParentDTO
    {
        public string? StudentCode { get; set; }
        public string Fullname { get; set; } = null!;
        public string? BloodType { get; set; }
        public int Classid { get; set; }
        public int Parentid { get; set; }
        public int Age { get; set; }
        public DateOnly Dob { get; set; }
        public bool Gender { get; set; }
    }
}
