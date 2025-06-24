using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.Students;

namespace DataAccessLayer.DTO
{
    public class VaccinationRecordStudentEvent
    {
        public int Vaccinationrecordid { get; set; }

        public int Studentid { get; set; }

        public int Vaccinationeventid { get; set; }

        public string Vaccinename { get; set; } = null!;

        public int Dosenumber { get; set; }

        public DateOnly Vaccinationdate { get; set; }

        public bool Confirmedbyparent { get; set; }

        public bool Isdeleted { get; set; }
        public List<StudentDTO> Students { get; set; } = new List<StudentDTO>();
        public List<VaccinationEventDTO> Vaccinationevents { get; set; } = new List<VaccinationEventDTO>();
    }
}
