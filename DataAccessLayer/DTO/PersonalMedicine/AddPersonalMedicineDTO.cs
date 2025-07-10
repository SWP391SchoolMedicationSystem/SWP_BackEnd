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

        public int Parentid { get; set; }

        public string Medicinename { get; set; } = null!;

        public int Quantity { get; set; }
        public DateTime Expirydate { get; set; }


        public string Createdby { get; set; } = null!;


    }
}
