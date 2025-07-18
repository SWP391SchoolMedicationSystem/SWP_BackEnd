using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;

namespace BussinessLayer.IService
{
    public interface IMedicalScheduleService
    {
        public Task<bool> AssignSchedule(MedicalScheduleDTO dto);
        public Task<bool> UpdateSchedule(MedicalScheduleDTO dto);
        public Task<bool> DeleteSchedule(int detailID, int medicineID);
        public Task<List<MedicalScheduleDTO>> GetAllSchedules();
        public Task<List<MedicalScheduleDTO>> GetSchedulesByWeeks();

    }
}
