using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.DTO.PersonalMedicine
{
    public class PersonalMedicineDTO
    {
        public int Personalmedicineid { get; set; }

        public int Studentid { get; set; }

        public string Medicinename { get; set; } = null!;

        public int Quanttiy { get; set; }

        public DateTime Receivedate { get; set; }

        public DateTime Expirydate { get; set; }

        public int Staffid { get; set; }

        public bool Isdeleted { get; set; }

        public string? Createdby { get; set; }

        public DateTime? Createddate { get; set; }

        public string? Modifiedby { get; set; }

        public DateTime? Modifieddate { get; set; }

    }
}
