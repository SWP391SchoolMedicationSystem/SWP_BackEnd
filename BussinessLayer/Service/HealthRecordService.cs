using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Utils.Configurations;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.HealthCheck;
using DataAccessLayer.DTO.HealthRecords;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace BussinessLayer.Service
{
    public class HealthRecordService : IHealthRecordService
    {
        private readonly IHealthRecordRepository _healthRecordRepository;
        private readonly IVaccinationRecordRepository _ivaccinationRecordRepository;
        private readonly IHealthCheckRepo _healthCheckRepository;
        private readonly IStudentRepo _studentRepository;
        private readonly IHealthCategoryRepo _healthCategoryRepo;
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;
        private readonly IHealthCheckEventRecordService _healthCheckEventRecordService;

        public HealthRecordService(IHealthRecordRepository healthRecordRepository,IVaccinationRecordRepository vaccinationRecordRepo,
            IHealthCheckRepo healthCheckRepository, IStudentRepo studentRepository,IHealthCategoryRepo healthCategoryRepo,
            IStaffRepository staffRepository,IParentRepository parentRepository,
            IMapper mapper,
            IOptionsMonitor<AppSetting> option, IHttpContextAccessor httpContextAccessor
            , IHealthCheckEventRecordService healthCheckEventRecordService
            )
        {
            _healthRecordRepository = healthRecordRepository;
            _ivaccinationRecordRepository = vaccinationRecordRepo;
            _healthCheckRepository = healthCheckRepository;
            _mapper = mapper;
            _studentRepository = studentRepository;
            _healthCategoryRepo = healthCategoryRepo;
            _staffRepository = staffRepository;
            _healthCheckEventRecordService = healthCheckEventRecordService;
        }
        public Task AddHealthRecordAsync(CreateHealthRecordDTO healthRecorddto)
        {
            if (healthRecorddto != null)
            {
                Healthrecord healthRecord = _mapper.Map<Healthrecord>(healthRecorddto);
                _healthRecordRepository.AddAsync(healthRecord);
                _healthRecordRepository.Save();
            }
            return Task.CompletedTask;
        }

        public async Task DeleteHealthRecord(int id)
        {
            var entity = await _healthRecordRepository.GetByIdAsync(id);

            if (entity != null)
            {
                _healthRecordRepository.Delete(id);
                _healthRecordRepository.Save();
            }
        }

        public async Task<List<Healthrecord>> GetAllHealthRecordsAsync()
        {
            List<Healthrecord> healthRecords = _mapper.Map<List<Healthrecord>>(await _healthRecordRepository.GetAllAsync());
            return healthRecords;
        }

        public async Task<HealthRecordDto> GetHealthRecordByIdAsync(int id)
        {
            HealthRecordDto healthRecord = _mapper.Map<HealthRecordDto>(await _healthRecordRepository.GetByIdAsync(id));
            return healthRecord;
        }

        public async Task<List<HealthRecordStudentCheck>> GetHealthRecords()
        {
            var healthrecordList = _healthRecordRepository.GetAllAsync().Result;
            List<HealthRecordStudentCheck> healthRecordStudentChecks = new List<HealthRecordStudentCheck>();
            foreach (var healthRecord in healthrecordList)
            {
                var student = await _studentRepository.GetByIdAsync(healthRecord.Studentid);
                var staff = await _staffRepository.GetByIdAsync(healthRecord.Staffid);
                var healthCategory = await _healthCategoryRepo.GetByIdAsync(healthRecord.Healthcategoryid);
                var vaccinationRecords = await _ivaccinationRecordRepository.GetRecordsByStudentAsync(healthRecord.Studentid);
                var healthChecks = await _healthCheckEventRecordService.GetHealthCheckRecordEventsByStudentIdAsync(healthRecord.Studentid) ;
                HealthRecordStudentCheck check = new HealthRecordStudentCheck
                {
                    HealthRecordId = healthRecord.Healthrecordid,
                    StudentName = student.Fullname,
                    HealthCategory = healthCategory.Healthcategoryname,
                    HealthRecordDate = healthRecord.Healthrecorddate,
                    Healthrecordtitle = healthRecord.Healthrecordtitle,
                    Healthrecorddescription = healthRecord.Healthrecorddescription ?? string.Empty,
                    StaffName = staff.Fullname,
                    IsConfirm = healthRecord.Isconfirm,
                    VaccinationRecords = _mapper.Map<List<VaccinationRecordDTO>>(vaccinationRecords),
                    HealthChecks = healthChecks.FirstOrDefault()
                };
                healthRecordStudentChecks.Add(check);
            }
            return healthRecordStudentChecks;
        }

        public Task<List<Healthrecord>> GetHealthRecordsByStudentIdAsync(int studentId)
        {
            var healthRecords = _healthRecordRepository.GetAllAsync().Result;
                var news = healthRecords.Where(c => c.Studentid == studentId)
                .ToList(); // Convert IEnumerable to List explicitly
            return Task.FromResult(news); // Wrap the result in a Task
        }

        public async Task<HealthRecordStudentCheck> GetHealthRecordsByStudentIdWithCheckAsync(int studentId)
        {
            var List = _healthRecordRepository.GetAll();
            var healthrecordList = List.FirstOrDefault(h => h.Studentid == studentId);
            List<HealthRecordStudentCheck> healthRecordStudentChecks = new List<HealthRecordStudentCheck>();

                var student = _studentRepository.GetByIdAsync(healthrecordList.Studentid).Result;
                var staff = _staffRepository.GetByIdAsync(healthrecordList.Staffid).Result;
                var healthCategory = _healthCategoryRepo.GetByIdAsync(healthrecordList.Healthcategoryid).Result;
                var vaccinationRecords = _ivaccinationRecordRepository.GetRecordsByStudentAsync(healthrecordList.Studentid).Result;
            var healthChecks = await _healthCheckEventRecordService.GetHealthCheckRecordEventsByStudentIdAsync(studentId);
            HealthRecordStudentCheck check = new HealthRecordStudentCheck
                {
                    HealthRecordId = healthrecordList.Healthrecordid,
                    StudentName = student.Fullname,
                    HealthCategory = healthCategory.Healthcategoryname,
                    HealthRecordDate = healthrecordList.Healthrecorddate,
                    Healthrecordtitle = healthrecordList.Healthrecordtitle,
                    Healthrecorddescription = healthrecordList.Healthrecorddescription ?? string.Empty,
                    StaffName = staff.Fullname,
                    IsConfirm = healthrecordList.Isconfirm,
                    VaccinationRecords = _mapper.Map<List<VaccinationRecordDTO>>(vaccinationRecords),
                HealthChecks = healthChecks.FirstOrDefault()
            };

            return check;
        }

        public async Task UpdateHealthRecord(UpdateHealthRecordDTO healthRecorddto, int id)
        {
            var entity = await _healthRecordRepository.GetByIdAsync(id);
            if (entity != null)
            {
                entity.Studentid = healthRecorddto.StudentID;
                entity.Healthcategoryid = healthRecorddto.HealthCategoryID;
                entity.Healthrecorddate = healthRecorddto.HealthRecordDate;
                entity.Healthrecordtitle = healthRecorddto.Healthrecordtitle;
                entity.Healthrecorddescription = healthRecorddto.Healthrecorddescription;
                entity.Staffid = healthRecorddto.Staffid;
                entity.Isconfirm = healthRecorddto.IsConfirm;
                entity.Modifiedby = healthRecorddto.ModifiedBy;
                entity.Modifieddate = DateTime.Now;
                _healthRecordRepository.Update(entity);
                _healthRecordRepository.Save();
            }
        }

    }
}
