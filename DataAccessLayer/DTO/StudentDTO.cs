using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class StudentDTO
    {
        public int Studentid { get; set; }

        public string Fullname { get; set; } = null!;
        public int Age { get; set; }

        public DateOnly Dob { get; set; }

        public bool Gender { get; set; }
    }
}
