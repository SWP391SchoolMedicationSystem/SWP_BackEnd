using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class MedicalScheduleService(IMedicineScheduleRepository medicineScheduleRepository, IMapper mapper) : IMedicalScheduleService
    {
        public async Task<bool> AssignSchedule(MedicalScheduleDTO dto)
        {
           var medicalSchedule = mapper.Map<Medicineschedule>(dto);
            await medicineScheduleRepository.AddAsync(medicalSchedule);
            medicineScheduleRepository.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateSchedule(MedicalScheduleDTO dto)
        {
            var medicalSchedule = mapper.Map<Medicineschedule>(dto);
            medicineScheduleRepository.Update(medicalSchedule);
            await medicineScheduleRepository.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteSchedule(int detailID, int medicineID)
        {
            var medicalScheduleList = await medicineScheduleRepository.GetAllAsync();
            var medicalSchedule = medicalScheduleList.FirstOrDefault(m => m.Scheduledetails == detailID && m.Personalmedicineid == medicineID);
            if (medicalSchedule != null)
            {
                medicineScheduleRepository.Delete(medicalSchedule.Schedulemedicineid);
                await medicineScheduleRepository.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<MedicalScheduleDTO>> GetAllSchedules()
        {
            var medicalSchedules = await medicineScheduleRepository.GetAllAsync();
            return mapper.Map<List<MedicalScheduleDTO>>(medicalSchedules);
        }
        public async Task<List<MedicalScheduleDTO>> GetSchedulesByWeeks()
        {
            var medicalSchedules = await medicineScheduleRepository.GetAllAsync();
            var currentDate = DateTime.Now;
            var weekStart = currentDate.AddDays(-(int)currentDate.DayOfWeek);
            var weekEnd = weekStart.AddDays(6);
            var weeklySchedules = medicalSchedules.Where(m => m.Startdate >= weekStart && m.Startdate <= weekEnd).ToList();
            return mapper.Map<List<MedicalScheduleDTO>>(weeklySchedules);
        }
    }
}
