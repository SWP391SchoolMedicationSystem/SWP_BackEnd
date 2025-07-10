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

        public bool IsDeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedByUserId { get; set; }

        public int? ModifiedByUserId { get; set; }
    }
}
