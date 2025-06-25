using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Students
{
    public class StudentInformation
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; } = null!;
        public DateOnly Dob { get; set; }
        public bool Gender { get; set; }
        public string Classname { get; set; } = null!;
        public string ParentName { get; set; } = null!;
        public string ParentPhone { get; set; } = null!;
        public string HealthStatus { get; set; } = null!;
        public string HealthRecord { get; set; } = null!;
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string? Notes { get; set; }


    }
}
