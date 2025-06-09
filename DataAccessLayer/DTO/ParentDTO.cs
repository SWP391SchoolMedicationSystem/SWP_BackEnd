using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class ParentDTO
    {
        public int Parentid { get; set; }

        public string Fullname { get; set; } = null!;

        public string? Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public List<StudentDTO> Students { get; set; } = new List<StudentDTO>();
    }
}
