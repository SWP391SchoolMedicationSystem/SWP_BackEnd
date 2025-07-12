using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.PersonalMedicine
{
    public class ApprovalPersonalMedicineDTO
    {

        public string DeliveryStatus { get; set; } = null!;

        public int ApprovedBy { get; set; }


    }
}
