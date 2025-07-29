using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Parents
{
    public class ParentVaccineEvent
    {
        [Required(ErrorMessage ="Cần nhập ParentID")]
        public int ParentId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string Address { get; set; } = null!;
        public List<StudentVaccineEvent> Students { get; set; } = new List<StudentVaccineEvent>();
    }

    public class  StudentVaccineEvent
    {
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string Fullname { get; set; } = null!;
        public int Age { get; set; }
        public DateOnly Dob { get; set; }
        public bool Gender { get; set; }
    }
}
