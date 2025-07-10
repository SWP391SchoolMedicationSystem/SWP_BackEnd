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
        public string StudentName { get; set; } = null!;
        public string ClassName { get; set; } = null!;
        public int? ParentId { get; set; }
        public string ParentName { get; set; } = null!;
        public string MedicineName { get; set; } = null!;
        public int Quantity { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public bool status { get; set; }
        public string Note { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public List<ScheduleDetailDTO> PreferedTime { get; set; } = new List<ScheduleDetailDTO>();
        public bool isApproved { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
