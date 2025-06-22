using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.PersonalMedicine
{
    public class UpdatePersonalMedicineDTO
    {
        public int Personalmedicineid { get; set; }
        public int Studentid { get; set; }

        public string Medicinename { get; set; } = null!;

        public int Quanttiy { get; set; }

        public DateTime Receivedate { get; set; }

        public DateTime Expirydate { get; set; }

        public int Staffid { get; set; }

        public bool Isdeleted { get; set; }

        public string? Modifiedby { get; set; }

        public DateTime? Modifieddate { get; set; }
    }
}
