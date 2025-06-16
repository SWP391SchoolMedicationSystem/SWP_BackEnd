using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.DTO
{
    public class UserInfoDTO
    {
        public int UserId { get; set; }

        public bool isStaff { get; set; } = false;

        public string Email { get; set; } = null!;

        public virtual ICollection<Parent> Parents { get; set; } = new List<Parent>();

        public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
    }
}
