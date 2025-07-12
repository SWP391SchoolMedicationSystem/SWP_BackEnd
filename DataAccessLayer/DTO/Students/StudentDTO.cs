using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.Parents;
using DataAccessLayer.Entity;

namespace DataAccessLayer.DTO.Students
{
    public class StudentDTO
    {
        public int Studentid { get; set; }

        public string? StudentCode { get; set; } = null!;

        public string Fullname { get; set; } = null!;

        public int Age { get; set; }

        public DateOnly Dob { get; set; }

        public string Gender { get; set; } = null!;

        public int Classid { get; set; }
        public string ClassName { get; set; } = null!;

        public int Parentid { get; set; }

        public string ParentName { get; set; } = null!;
        public string ParentPhone { get; set; } = null!;

        public bool IsDeleted { get; set; }


        public string? BloodType { get; set; }

        public bool Isdeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedByUserId { get; set; }

        public string? CreatedByUserName { get; set; } = null!;

        public int? ModifiedByUserId { get; set; }

        public string? ModifiedByUserName { get; set; } = null!;

    }
}
