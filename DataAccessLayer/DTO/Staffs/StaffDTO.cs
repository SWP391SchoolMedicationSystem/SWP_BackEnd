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

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public int Roleid { get; set; }

        public int Userid { get; set; }

        public bool Isdeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedByUserId { get; set; }

        public string? CreatedByUserName { get; set; } = null!;

        public int? ModifiedByUserId { get; set; }

        public string? ModifiedByUserName { get; set; } = null!;
    }
}
