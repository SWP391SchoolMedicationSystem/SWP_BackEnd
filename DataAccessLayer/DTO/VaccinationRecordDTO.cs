using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class VaccinationRecordDTO
    {
        public string Createdby { get; set; } = null!;

        public int Studentid { get; set; }

        public int Vaccinationeventid { get; set; }

        public string Vaccinename { get; set; } = null!;

        public int Dosenumber { get; set; }

        public DateOnly Vaccinationdate { get; set; }

        public bool Confirmedbyparent { get; set; }

        public bool? Willattend { get; set; }

        public string? Reasonfordecline { get; set; }

        public bool? Parentconsent { get; set; }

        public DateTime? Responsedate { get; set; }

    }
}
