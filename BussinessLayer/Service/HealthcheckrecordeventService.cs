using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO.HealthCheck;
using DataAccessLayer.DTO.HealthRecords;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;

namespace BussinessLayer.Service
{
    public class HealthcheckrecordeventService(IHealthcheckrecordeventRepository healthCheckEventRepository, IMapper mapper, IClassRoomRepository classRoomRepository, IStudentRepo studentRepo) : IHealthCheckEventRecordService
    {
        public async Task<List<HealthCheckDtoIgnoreClass>> GetAllHealthCheckRecordEventsAsync()
        {
            var classroom = await classRoomRepository.GetAllAsync();

            var healthcheck = await healthCheckEventRepository.GetAllAsync();
            var students = await studentRepo.GetAllAsync();
            var list = mapper.Map<List<HealthCheckDtoIgnoreClass>>(healthcheck);
            foreach(var item in list)
            {
                var student = students.FirstOrDefault(x => x.Studentid == item.Healthcheckrecord.Studentid);
                item.ClassName = student.Class.Classname;
            }
            return list.OrderBy(x => x.Healthcheckevent.Eventdate).Reverse().ToList();
        }
        public async Task<Healthcheckrecordevent?> GetHealthCheckRecordEventByIdAsync(int eventId)
        {
            return await healthCheckEventRepository.GetByIdAsync(eventId);
        }
        public async Task AddHealthCheckRecordEventAsync(AddHealthcheckrecordeventDTO healthCheckRecordEvent)
        {
            try
            {
                var record = mapper.Map<Healthcheckrecordevent>(healthCheckRecordEvent);
                await healthCheckEventRepository.AddAsync(record);
                await healthCheckEventRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Error adding Health Check and Event Together {e.Message}");
            }

        }
        public async Task UpdateHealthCheckRecordEventAsync(Healthcheckrecordevent healthCheckRecordEvent)
        {
            var healthcheck = await healthCheckEventRepository.GetByIdAsync(healthCheckRecordEvent.Healthcheckid);
            if (healthcheck != null)
            {
                healthcheck.Healthcheckid = healthCheckRecordEvent.Healthcheckid;
                healthcheck.Healthcheckeventid = healthCheckRecordEvent.Healthcheckeventid;
                healthCheckEventRepository.Update(healthcheck);
                await healthCheckEventRepository.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Healthcheckrecordevent not found.");
            }
        }
        public async Task DeleteHealthCheckRecordEventAsync(int eventId)
        {
            var healthcheck = await healthCheckEventRepository.GetByIdAsync(eventId);
            healthcheck.Isdeleted = true; // Soft delete
            healthCheckEventRepository.Update(healthcheck);
            await healthCheckEventRepository.SaveChangesAsync();
        }

        public async Task<List<HealthCheckDtoIgnoreClass>> GetHealthCheckRecordEventsByStudentIdAsync(int studentId)
        {
            var classroom = await classRoomRepository.GetAllAsync();

            var healthcheck = await healthCheckEventRepository.GetAllAsync();
            var students = await studentRepo.GetAllAsync();
            var list = mapper.Map<List<HealthCheckDtoIgnoreClass>>(healthcheck);
            foreach (var item in list)
            {
                var student = students.FirstOrDefault(x => x.Studentid == item.Healthcheckrecord.Studentid);
                item.ClassName = student.Class.Classname;
            }

            return list.Where(x => x.Healthcheckrecord.Studentid == studentId).OrderBy(x => x.Healthcheckevent.Eventdate).Reverse().ToList();
        }

        public async Task<List<Healthcheckrecordevent>> GetHealthCheckRecordEventsByEventIdAsync(int eventId)
        {
            var list = await healthCheckEventRepository.GetAllAsync();
            return list.Where(x => x.Healthcheckevent.Healthcheckeventid == eventId).OrderBy(x => x.Healthcheckevent.Eventdate).Reverse().ToList();
        }
       
    }
}
