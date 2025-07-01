using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.Medicines;

namespace BussinessLayer.IService
{
    public interface IMedicineScheduleService 
    {
        Task AddMedicineScheduleAsync(AddScheduleMedicineDTO medicineSchedule);
        Task DeleteMedicineScheduleAsync(int id);
        Task<List<MedicineScheduleDTO>> GetAllMedicineSchedulesAsync();
        Task<MedicineScheduleDTO> GetMedicineScheduleByIdAsync(int id);
        Task UpdateMedicineScheduleAsync(MedicineScheduleDTO medicineSchedule);
        Task<List<MedicineScheduleDTO>> GetMedicineSchedulesByPersonalMedicineIdAsync(int personalMedicineId);
        Task<List<MedicineScheduleDTO>> GetMedicineSchedulesByScheduledetailsAsync(int scheduledetails);
        Task<List<MedicineScheduleDTO>> GetMedicineSchedulesByStudentID(int studentId);
    }
}
