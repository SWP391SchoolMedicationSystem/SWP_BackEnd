using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class VaccinationEventDTO
    {
        public string Vaccinationeventname { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string Organizedby { get; set; } = null!;

        public bool Isdeleted { get; set; }
    }
}
