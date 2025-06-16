using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Staffs
{
    public class StaffUpdate
    {
        public int Staffid { get; set; }

        public string Fullname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int Phone { get; set; }
        public int Roleid { get; set; }
    }
}
