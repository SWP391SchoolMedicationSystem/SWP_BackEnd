using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.Students;

namespace DataAccessLayer.DTO.Parents
{
    public class ParentDTO
    {
        public int Parentid { get; set; }

        public string Fullname { get; set; } = null!;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string Address { get; set; } = null!;

        public bool Isdeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedByUserId { get; set; }

        public string? CreatedByUserName { get; set; } = null!;

        public int? ModifiedByUserId { get; set; }

        public string? ModifiedByUserName { get; set; } = null!;

        public List<StudentDTO> Students { get; set; } = new List<StudentDTO>();
    }
}
