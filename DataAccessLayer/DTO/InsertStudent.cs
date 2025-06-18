using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class InsertStudent
    {
        public string studentCode { get; set; } = null!;
        public string fullName { get; set; } = null!;
        public string bloodtype { get; set; } = null!;
        public string className { get; set; } = null!;
        public string parentName { get; set; } = null!;
        public string parentphone { get; set; } = null!;
        public DateOnly birthDate { get; set; }
        public string gender { get; set; } = null!;
        public string healthStatus { get; set; } = null!;

    }
}
