using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class UserDTo
    {
        public int UserId { get; set; }

        public bool isStaff { get; set; } = false;

        public string Email { get; set; } = null!;

        public byte[] Hash { get; set; } = null!;

        public byte[] Salt { get; set; } = null!;
    }
}
