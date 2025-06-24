using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.PersonalMedicine
{
    public class AddPersonalMedicineDTO
    {
        public int Studentid { get; set; }

        public string Medicinename { get; set; } = null!;

        public int Quanttiy { get; set; }

        public DateTime Receivedate { get; set; }

        public DateTime Expirydate { get; set; }

        public int Staffid { get; set; }

        public bool Isdeleted { get; set; }

        public string? Createdby { get; set; }

        public DateTime? Createddate { get; set; }

    }
}
