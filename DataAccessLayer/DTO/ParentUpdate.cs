using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class ParentUpdate
    {
        public int Parentid { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

        public int Phone { get; set; } 

        public string Address { get; set; } 

    }
}
