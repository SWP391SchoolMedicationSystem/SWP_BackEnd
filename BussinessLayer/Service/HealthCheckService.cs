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

        public async Task<Healthcheck> AddHealthCheckAsync(HealthCheckDTO healthCheckDto)
        {
            if (_staffservice.GetStaffByIdAsync(healthCheckDto.Staffid) != null
                && _studentService.GetAllStudentsAsync()
                    .Result.FirstOrDefault(s => s.Studentid == healthCheckDto.Studentid) != null)
            {
                Healthcheck healthcheck = _mapper.Map<Healthcheck>(healthCheckDto);
                await _healthCheckRepository.AddAsync(healthcheck); // Use AddAsync instead of Add
                healthcheck.Createdat = DateTime.Now; // Set the creation date
                _healthCheckRepository.Save(); // Ensure changes are saved
                return healthcheck; // Return the created healthcheck object
            }

            return null; // Return null if the conditions are not met
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
                // Ensure changes are saved
            }
        }

        public async Task<List<HealthCheckDTO>> GetAllHealthChecksAsync()
        {
            var listhealth =  await _healthCheckRepository.GetAllAsync();
            return _mapper.Map<List<HealthCheckDTO>>(listhealth);
        }

        public async Task<Healthcheck> UpdateHealthCheckAsync(HealthCheckDTO healthCheckDto)
        {
                Healthcheck check = await _healthCheckRepository.GetByIdAsync(healthCheckDto.Checkid);
                if (check != null)
                    if(!string.IsNullOrEmpty(healthCheckDto.Notes))
                {
                    check.Notes = healthCheckDto.Notes;
                }
            if (healthCheckDto.Height.HasValue)
            {
                check.Height = healthCheckDto.Height.Value;
            }
            if (healthCheckDto.Weight.HasValue)
            {
                check.Weight = healthCheckDto.Weight.Value;
            }
            if (healthCheckDto.Visionleft.HasValue)
            {
                check.Visionleft = healthCheckDto.Visionleft.Value;
            }
            if (healthCheckDto.Visionright.HasValue)
            {
                check.Visionright = healthCheckDto.Visionright.Value;
            }
            if (!string.IsNullOrEmpty(healthCheckDto.Bloodpressure))
            {
                check.Bloodpressure = healthCheckDto.Bloodpressure;
            }
            check.Updatedat = DateTime.Now; // Update the timestamp
            _healthCheckRepository.Update(check); // Use Update method to save changes
            _healthCheckRepository.Save(); // Ensure changes are saved
            return check; // Return the updated healthcheck object
        
                    // Ensure changes are saved
                }
        }
    }

