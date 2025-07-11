using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO.HealthChecks;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;

namespace BussinessLayer.Service
{
    public class HealthCheckService : IHealthCheckService
    {
        private readonly IStudentService _studentService;
        private readonly IStaffService _staffservice;
        private readonly IHealthCheckRepo _healthCheckRepository;
        private IMapper _mapper;
        public HealthCheckService(
            IStudentService studentService,
            IStaffService staffservice,
            IHealthCheckRepo healthCheckRepository,
            IMapper mapper)
        {
            _studentService = studentService;
            _staffservice = staffservice;
            _healthCheckRepository = healthCheckRepository;
            _mapper = mapper;
        }

        public async Task<Healthcheck> AddHealthCheckAsync(AddHealthCheckDTO healthCheckDto)
        {
            if (_staffservice.GetStaffByIdAsync(healthCheckDto.Staffid) != null
                && _studentService.GetAllStudentsAsync()
                    .Result.FirstOrDefault(s => s.Studentid == healthCheckDto.Studentid) != null)
            {

                if (healthCheckDto.Visionleft == 10) healthCheckDto.Visionleft = (decimal?)9.99;
                if (healthCheckDto.Visionright == 10) healthCheckDto.Visionright = (decimal)9.99;
                Healthcheck healthcheck = _mapper.Map<Healthcheck>(healthCheckDto);
                healthcheck.CreatedAt = DateTime.Now;

                await _healthCheckRepository.AddAsync(healthcheck); // Use AddAsync instead of Add
                // Set the creation date
                _healthCheckRepository.Save(); // Ensure changes are saved
                return healthcheck; // Return the created healthcheck object
            }

            return null; 
        }

        public async Task<bool> DeleteHealthCheckAsync(int checkId)
        {
            Healthcheck check = await _healthCheckRepository.GetByIdAsync(checkId);
            if (check == null)
            {
                return false; // Return false if the health check does not exist
            }
            else
            {
                check.Isdeleted = true;
                _healthCheckRepository.Save();
                return true;
            }
        }

        public async Task<List<HealthCheckDTO>> GetAllHealthChecksAsync()
        {
            var listhealth = await _healthCheckRepository.GetAllAsync();
            var studentList = await _studentService.GetAllStudentsAsync();
            var staffList = await _staffservice.GetAllStaffAsync();

            var result = listhealth
                .Where(healthcheck => listhealth.Any(h => h.Checkid == healthcheck.Checkid))
                .Select(healthcheck => new HealthCheckDTO
                {
                    Checkid = healthcheck.Checkid,
                    Staffid = healthcheck.Staffid,
                    StaffName = staffList.FirstOrDefault(s => s.Staffid == healthcheck.Staffid)?.Fullname ?? "Unknown Staff",
                    Studentid = healthcheck.Studentid,
                    StudentName = studentList.FirstOrDefault(s => s.Studentid == healthcheck.Studentid)?.Fullname ?? "Unknown Student",
                    Notes = healthcheck.Notes,
                    Height = healthcheck.Height,
                    Weight = healthcheck.Weight,
                    Visionleft = healthcheck.Visionleft,
                    Visionright = healthcheck.Visionright,
                    Bloodpressure = healthcheck.Bloodpressure,
                    Checkdate = healthcheck.Checkdate
                })
                .ToList();

            return result;
        }

        public async Task<Healthcheck> UpdateHealthCheckAsync(UpdateHealthCheckDTO healthCheckDto)
        {
            Healthcheck check = await _healthCheckRepository.GetByIdAsync(healthCheckDto.Checkid);
            if (check != null)
            {
                    check.Notes = healthCheckDto.Notes;
                    check.Height = healthCheckDto.Height.Value;
                    check.Weight = healthCheckDto.Weight.Value;
                    check.Visionleft = healthCheckDto.Visionleft.Value;
                    check.Visionright = healthCheckDto.Visionright.Value;
                    check.Bloodpressure = healthCheckDto.Bloodpressure;
                check.Studentid = healthCheckDto.Studentid;
                check.Staffid = healthCheckDto.Staffid;
                check.Checkdate = healthCheckDto.Checkdate;
                check.Isdeleted = healthCheckDto.Isdeleted; // Update Isdeleted status
                check.ModifiedByUserId = healthCheckDto.ModifiedByUserId; // Update ModifiedByUserId
                check.ModifiedAt = DateTime.Now; // Update the timestamp
            }
            _healthCheckRepository.Update(check); // Use Update method to save changes
            _healthCheckRepository.Save(); // Ensure changes are saved
            return check; // Return the updated healthcheck object
        }
        public async Task<Healthcheck> GetHealthCheckByIdAsync(int checkId)
        {
            return await _healthCheckRepository.GetByIdAsync(checkId);
        }
        public async Task<List<HealthCheckDTO>> GetHealthChecksByStudentIdAsync(int studentId)
        {
            var healthChecks = await _healthCheckRepository.GetAllAsync();
            var studentHealthChecks = healthChecks.Where(h => h.Studentid == studentId).ToList();
            return _mapper.Map<List<HealthCheckDTO>>(studentHealthChecks);
        }
    }
}

