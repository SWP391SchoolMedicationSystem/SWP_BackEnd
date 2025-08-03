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

        public HealthRecordService(IHealthRecordRepository healthRecordRepository,IVaccinationRecordRepository vaccinationRecordRepo,
            IHealthCheckRepo healthCheckRepository, IStudentRepo studentRepository,IHealthCategoryRepo healthCategoryRepo,
            IStaffRepository staffRepository,IParentRepository parentRepository,
            IMapper mapper,
            IOptionsMonitor<AppSetting> option, IHttpContextAccessor httpContextAccessor)
        {
            _healthRecordRepository = healthRecordRepository;
            _ivaccinationRecordRepository = vaccinationRecordRepo;
            _healthCheckRepository = healthCheckRepository;
            _mapper = mapper;
            _studentRepository = studentRepository;
            _healthCategoryRepo = healthCategoryRepo;
            _staffRepository = staffRepository;
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

        public void DeleteHealthRecord(int id)
        {
            var entity = _healthRecordRepository.GetByIdAsync(id).Result;

            if (entity != null)
            {
                entity.Isdeleted = true;
                _healthRecordRepository.Update(entity);
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

        public Task<List<HealthRecordStudentCheck>> GetHealthRecords()
        {
            var healthrecordList = _healthRecordRepository.GetAllAsync().Result;
            List<HealthRecordStudentCheck> healthRecordStudentChecks = new List<HealthRecordStudentCheck>();
            foreach (var healthRecord in healthrecordList)
            {
                var student = _studentRepository.GetByIdAsync(healthRecord.Studentid).Result;
                var staff = _staffRepository.GetByIdAsync(healthRecord.Staffid).Result;
                var healthCategory = _healthCategoryRepo.GetByIdAsync(healthRecord.Healthcategoryid).Result;
                var vaccinationRecords = _ivaccinationRecordRepository.GetRecordsByStudentAsync(healthRecord.Studentid).Result;
                var healthChecks = _healthCheckRepository.GetHealthChecksByStudentIdAsync(healthRecord.Studentid).Result;
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
                    HealthChecks = _mapper.Map<List<HealthCheckDTO>>(healthChecks)
                };
                healthRecordStudentChecks.Add(check);
            }
            return Task.FromResult(healthRecordStudentChecks);
        }

        public Task<List<Healthrecord>> GetHealthRecordsByStudentIdAsync(int studentId)
        {
            var healthRecords = _healthRecordRepository.GetAllAsync().Result;
                var news = healthRecords.Where(c => c.Studentid == studentId)
                .ToList(); // Convert IEnumerable to List explicitly
            return Task.FromResult(news); // Wrap the result in a Task
        }

        public Task<HealthRecordStudentCheck> GetHealthRecordsByStudentIdWithCheckAsync(int studentId)
        {
            var List = _healthRecordRepository.GetAll();
            var healthrecordList = List.FirstOrDefault(h => h.Studentid == studentId);
            List<HealthRecordStudentCheck> healthRecordStudentChecks = new List<HealthRecordStudentCheck>();

                var student = _studentRepository.GetByIdAsync(healthrecordList.Studentid).Result;
                var staff = _staffRepository.GetByIdAsync(healthrecordList.Staffid).Result;
                var healthCategory = _healthCategoryRepo.GetByIdAsync(healthrecordList.Healthcategoryid).Result;
                var vaccinationRecords = _ivaccinationRecordRepository.GetRecordsByStudentAsync(healthrecordList.Studentid).Result;
                var healthChecks = _healthCheckRepository.GetHealthChecksByStudentIdAsync(healthrecordList.Studentid).Result;
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
                    HealthChecks = _mapper.Map<List<HealthCheckDTO>>(healthChecks)
                };
            
            return Task.FromResult(check);
        }

        public void UpdateHealthRecord(UpdateHealthRecordDTO healthRecorddto, int id)
        {
            var entity = _healthRecordRepository.GetByIdAsync(id).Result;
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
