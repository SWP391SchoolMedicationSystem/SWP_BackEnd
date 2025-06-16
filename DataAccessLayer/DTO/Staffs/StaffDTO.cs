using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Staffs
{
    public class StaffDTO
    {
        public int Staffid { get; set; }

        public string Fullname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int Phone { get; set; }

        public int Roleid { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public int UserID { get; set; }
    }
}
