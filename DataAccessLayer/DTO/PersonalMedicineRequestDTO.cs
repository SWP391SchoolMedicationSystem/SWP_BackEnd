using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.DTO
{
    public class PersonalMedicineRequestDTO
    {
        public int? Studentid { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public int? ParentId { get; set; }
        public string ParentName { get; set; }
        public string MedicineName { get; set; }
        public string MedicineType { get; set; }
        public int Quantity { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public bool status { get; set; }
        public string Note { get; set; }
        public string PhoneNumber { get; set; }
        public List<Medicineschedule> PreferedTime { get; set; } = new List<Medicineschedule>();
        public bool isApproved { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
