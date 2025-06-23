using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IVaccinationEventService
    {
        Task<List<Vaccinationevent>> GetAllVaccinationEvents();
        Task<Vaccinationevent> GetVaccinationEventById(int id);
        Task AddVaccinationEventAsync(VaccinationEventDTO eventDto);
        void UpdateVaccinationEvent(VaccinationEventDTO eventDto);
        void DeleteVaccinationEvent(int id);

    }
}
