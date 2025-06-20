using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Parents
{
    public class ParentStudent
    {
        public int Parentid { get; set; }

        public string Fullname { get; set; } = null!;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string Address { get; set; } = null!;
    }
}
