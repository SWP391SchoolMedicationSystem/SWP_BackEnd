using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class DashboardDTO
    {
        public int totalUsers { get; set; }
        public int totalStaff { get; set; }
        public int totalParents { get; set; }
        public int totalStudents { get; set; }
        public int activeUsers { get; set; }
        public int totalManagers { get; set; }
        public int totalNurses { get; set; }
    }
}
