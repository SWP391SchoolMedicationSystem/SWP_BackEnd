using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.HealthCheck;
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
        private readonly IHealthCheckEventRecordService _healthCheckEventRecordService;
        private readonly IHealthCheckEventRepository _healthCheckEventRepository;
        private IMapper _mapper;
        public HealthCheckService(
            IStudentService studentService,
            IStaffService staffservice,
            IHealthCheckRepo healthCheckRepository,
            IMapper mapper,
            IHealthCheckEventRecordService healthCheckEventRecordService,
            IHealthCheckEventRepository healthCheckEventRepository,
            IHealthCheckRepo healthCheckRepo)
        {
            _studentService = studentService;
            _staffservice = staffservice;
            _healthCheckRepository = healthCheckRepository;
            _mapper = mapper;
            _healthCheckRepository = healthCheckRepo;
            _healthCheckEventRecordService = healthCheckEventRecordService;
            _healthCheckEventRepository = healthCheckEventRepository;
        }

        public async Task AddHealthCheckAsync(AddHealthCheckDto healthCheckDto)
        {
            var studentlist = await _studentService.GetAllStudentsAsync();
            if (await _staffservice.GetStaffByIdAsync(healthCheckDto.Staffid) != null
                && studentlist.FirstOrDefault(s => s.StudentId == healthCheckDto.Studentid) != null)
            {

                if (healthCheckDto.Visionleft == 10) healthCheckDto.Visionleft = (decimal?)9.99;
                if (healthCheckDto.Visionright == 10) healthCheckDto.Visionright = (decimal)9.99;
                Healthcheck healthcheck = _mapper.Map<Healthcheck>(healthCheckDto);
                healthcheck.Createdat = DateTime.Now;
                var healthCheckEvent = _healthCheckEventRepository.GetByIdAsync(healthCheckDto.Eventid);
                if (healthCheckEvent != null)
                {
                    await _healthCheckRepository.AddAsync(healthcheck);
                    await _healthCheckRepository.SaveChangesAsync();
                    AddHealthcheckrecordeventDTO addHealthcheckrecordeventDTO = new AddHealthcheckrecordeventDTO
                    {
                        Healthcheckrecordid = healthcheck.Checkid,
                        Healthcheckeventid = healthCheckDto.Eventid
                    };
                    await _healthCheckEventRecordService.AddHealthCheckRecordEventAsync(addHealthcheckrecordeventDTO);

                }

            }
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

        public List<HealthCheckDTO> GetAllHealthChecksAsync()
        {
            var listhealth = _healthCheckRepository.GetAllAsync().Result;
            var result = new List<HealthCheckDTO>();
            foreach (var healthcheck in listhealth)
            {
                {
                    if (_healthCheckRepository.GetAllAsync().Result.FirstOrDefault(h => h.Checkid == healthcheck.Checkid) != null)

                        result.Add(new HealthCheckDTO
                        {
                            Checkid = healthcheck.Checkid,
                            Staffid = healthcheck.Staffid,
                            Studentid = healthcheck.Studentid,
                            Notes = healthcheck.Notes,
                            Height = healthcheck.Height,
                            Weight = healthcheck.Weight,
                            Visionleft = healthcheck.Visionleft,
                            Visionright = healthcheck.Visionright,
                            Bloodpressure = healthcheck.Bloodpressure,
                            Checkdate = healthcheck.Checkdate
                        });
                }

            }
            return result;
        }

        public async Task<Healthcheck> UpdateHealthCheckAsync(HealthCheckDTO healthCheckDto)
        {
            Healthcheck check = await _healthCheckRepository.GetByIdAsync(healthCheckDto.Checkid);
            if (check != null)
            {
                if (!string.IsNullOrEmpty(healthCheckDto.Notes))
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
            }

            check.Updatedat = DateTime.Now; // Update the timestamp
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

