using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class ParentRegister
    {

        public string Fullname { get; set; } = null!;

        public string? Email { get; set; }

        public int Phone { get; set; }
        public string Password { get; set; } = null!;

        public string Address { get; set; } = null!;
    }
}
