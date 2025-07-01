using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO.Medicines;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;

namespace BussinessLayer.Service
{
    public class MedicineScheduleService(IMedicineScheduleRepository medicineScheduleRepository, IPersonalMedicineRepository personalMedicineRepository, IScheduleDetailRepo scheduleDetailRepo, IMapper mapper, 
        IStudentRepo studentRepo) : IMedicineScheduleService
    {
        public async Task AddMedicineScheduleAsync(AddScheduleMedicineDTO medicineSchedule)
        {
            if(medicineSchedule == null)
            {
                throw new ArgumentNullException(nameof(medicineSchedule), "Medicine schedule cannot be null");
            }
            if(await scheduleDetailRepo.GetByIdAsync(medicineSchedule.Scheduledetails) == null)
            {
                throw new ArgumentException("Invalid schedule details ID", nameof(medicineSchedule.Scheduledetails));
            }
            if(await personalMedicineRepository.GetByIdAsync(medicineSchedule.Personalmedicineid) == null)
            {
                throw new ArgumentException("Invalid personal medicine ID", nameof(medicineSchedule.Personalmedicineid));
            }
            if(medicineScheduleRepository.GetAllAsync().Result.
                FirstOrDefault(ms => ms.Scheduledetails == medicineSchedule.Scheduledetails && ms.Personalmedicineid == medicineSchedule.Personalmedicineid) != null)
            {
                throw new ArgumentException("Schedule already exist", nameof(medicineSchedule));
            }
            var shedule = mapper.Map<Medicineschedule>(medicineSchedule);
            await medicineScheduleRepository.AddAsync(shedule);
            medicineScheduleRepository.Save();
            
        }

        public async Task DeleteMedicineScheduleAsync(int id)
        {
            var medicineSchedule = await  medicineScheduleRepository.GetByIdAsync(id);
            if (medicineSchedule == null)
            {
                throw new ArgumentException("Medicine schedule not found", nameof(id));
            }
            medicineScheduleRepository.Delete(id);
            medicineScheduleRepository.Save();
        }

        public async Task<List<MedicineScheduleDTO>> GetAllMedicineSchedulesAsync()
        {
            var medicineschedules = await medicineScheduleRepository.GetAllAsync();
            if (medicineschedules == null || !medicineschedules.Any())
            {
                return new List<MedicineScheduleDTO>();
            }
            var dtos = mapper.Map<List<MedicineScheduleDTO>>(medicineschedules);
            return dtos;
        }

        public Task<MedicineScheduleDTO> GetMedicineScheduleByIdAsync(int id)
        {
            var medicineSchedule = medicineScheduleRepository.GetByIdAsync(id);
            if (medicineSchedule == null)
            {
                throw new ArgumentException("Medicine schedule not found", nameof(id));
            }
            var dto = mapper.Map<MedicineScheduleDTO>(medicineSchedule);
            return Task.FromResult(dto);
        }

        public Task<List<MedicineScheduleDTO>> GetMedicineSchedulesByPersonalMedicineIdAsync(int personalMedicineId)
        {
            var medicineSchedules = medicineScheduleRepository.GetAllAsync().Result
                .Where(ms => ms.Personalmedicineid == personalMedicineId).ToList();
            if (medicineSchedules == null || !medicineSchedules.Any())
            {
                return Task.FromResult(new List<MedicineScheduleDTO>());
            }
            var dtos = mapper.Map<List<MedicineScheduleDTO>>(medicineSchedules);
            return Task.FromResult(dtos);
        }

        public Task<List<MedicineScheduleDTO>> GetMedicineSchedulesByScheduledetailsAsync(int scheduledetails)
        {
            var medicineSchedules = medicineScheduleRepository.GetAllAsync().Result
                .Where(ms => ms.Scheduledetails == scheduledetails).ToList();
            if (medicineSchedules == null || !medicineSchedules.Any())
            {
                return Task.FromResult(new List<MedicineScheduleDTO>());
            }
            var dtos = mapper.Map<List<MedicineScheduleDTO>>(medicineSchedules);
            return Task.FromResult(dtos);
        }

        public Task<List<MedicineScheduleDTO>> GetMedicineSchedulesByStudentID(int studentId)
        {
            var personalMedicine = personalMedicineRepository.GetAllAsync().Result
                .Where(pm => pm.Studentid == studentId).ToList();
            var medicineSchedules = medicineScheduleRepository.GetAllAsync().Result
                .Where(ms => personalMedicine.Any(pm => pm.Personalmedicineid == ms.Personalmedicineid)).ToList();
            if (medicineSchedules == null || !medicineSchedules.Any())
            {
                return Task.FromResult(new List<MedicineScheduleDTO>());
            }
            var dtos = mapper.Map<List<MedicineScheduleDTO>>(medicineSchedules);
            return Task.FromResult(dtos);

        }

        public async Task UpdateMedicineScheduleAsync(MedicineScheduleDTO medicineSchedule)
        {
            var existingSchedule = await medicineScheduleRepository.GetByIdAsync(medicineSchedule.Schedulemedicineid);
            if (existingSchedule == null)
            {
                throw new ArgumentException("Medicine schedule not found", nameof(medicineSchedule.Schedulemedicineid));
            }
            if (medicineSchedule == null)
            {
                throw new ArgumentNullException(nameof(medicineSchedule), "Medicine schedule cannot be null");
            }
            existingSchedule.Scheduledetails = medicineSchedule.Scheduledetails;
            existingSchedule.Personalmedicineid = medicineSchedule.Personalmedicineid;
            existingSchedule.Notes = medicineSchedule.Notes;
            existingSchedule.Duration = medicineSchedule.Duration;
            existingSchedule.Startdate = medicineSchedule.Startdate;
            medicineScheduleRepository.Update(existingSchedule);
            medicineScheduleRepository.Save();

        }
    }
}
