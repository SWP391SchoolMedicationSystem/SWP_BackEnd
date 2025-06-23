using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IPersonalMedicineScheduleService
    {
        Task<List<Personalmedicineschedule>> GetAllPersonalMedicineSchedules();
        Task<Personalmedicineschedule> GetPersonalMedicineScheduleById(int id);
        Task AddPersonalMedicineScheduleAsync(PersonalMedicineScheduleDTO scheduleDto);
        void UpdatePersonalMedicineSchedule(PersonalMedicineScheduleDTO scheduleDto);
        void DeletePersonalMedicineSchedule(int id);

    }
}
