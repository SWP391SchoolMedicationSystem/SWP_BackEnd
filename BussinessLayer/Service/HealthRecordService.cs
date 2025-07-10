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
using DataAccessLayer.DTO.HealthRecords;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using static BussinessLayer.Utils.Constants;

namespace BussinessLayer.Service
{
    public class HealthRecordService : IHealthRecordService
    {
        private readonly IHealthRecordRepository _healthRecordRepository;
        private readonly IVaccinationRecordRepository _ivaccinationRecordRepository;
        private readonly IHealthCheckRepo _healthCheckRepository;
        private readonly IStudentRepo _studentRepository;
        private readonly IHealthRecordCategoryRepo _healthCategoryRepo;
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public HealthRecordService(IHealthRecordRepository healthRecordRepository,IVaccinationRecordRepository vaccinationRecordRepo,
            IHealthCheckRepo healthCheckRepository, IStudentRepo studentRepository,IHealthRecordCategoryRepo healthCategoryRepo,
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
                HealthRecord healthRecord = _mapper.Map<HealthRecord>(healthRecorddto);
                healthRecord.Status = FormStatus.Pending;
                healthRecord.CreatedAt = DateTime.Now;  
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
                entity.Status = FormStatus.Deleted;
                _healthRecordRepository.Update(entity);
                _healthRecordRepository.Save();
            }
        }

        public async Task<List<HealthRecord>> GetAllHealthRecordsAsync()
        {
            List<HealthRecord> healthRecords = _mapper.Map<List<HealthRecord>>(await _healthRecordRepository.GetAllAsync());
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
                var student = _studentRepository.GetByIdAsync(healthRecord.StudentId).Result;
                var staff = _staffRepository.GetByIdAsync(healthRecord.StaffId).Result;
                var healthCategory = _healthCategoryRepo.GetByIdAsync(healthRecord.HealthCategoryId).Result;
                var vaccinationRecords = _ivaccinationRecordRepository.GetRecordsByStudentAsync(healthRecord.StudentId).Result;
                var healthChecks = _healthCheckRepository.GetHealthChecksByStudentIdAsync(healthRecord.StudentId).Result;
                HealthRecordStudentCheck check = new HealthRecordStudentCheck
                {
                    StudentName = student.Fullname,
                    HealthCategory = healthCategory.CategoryName,
                    HealthRecordDate = healthRecord.HealthRecordDate,
                    Healthrecordtitle = healthRecord.HealthRecordTitle,
                    Healthrecorddescription = healthRecord.HealthRecordDescription ?? string.Empty,
                    StaffName = staff.Fullname,
                    Status = healthRecord.Status,
                    VaccinationRecords = _mapper.Map<List<VaccinationRecordDTO>>(vaccinationRecords),
                    HealthChecks = _mapper.Map<List<HealthCheckDTO>>(healthChecks)
                };
                healthRecordStudentChecks.Add(check);
            }
            return Task.FromResult(healthRecordStudentChecks);
        }

        public Task<List<HealthRecord>> GetHealthRecordsByStudentIdAsync(int studentId)
        {
            var healthRecords = _healthRecordRepository.GetAllAsync().Result;
                var news = healthRecords.Where(c => c.StudentId == studentId)
                .ToList(); // Convert IEnumerable to List explicitly
            return Task.FromResult(news); // Wrap the result in a Task
        }

        public Task<HealthRecordStudentCheck> GetHealthRecordsByStudentIdWithCheckAsync(int studentId)
        {
            var List = _healthRecordRepository.GetAll();
            var healthrecordList = List.FirstOrDefault(h => h.StudentId == studentId);
            List<HealthRecordStudentCheck> healthRecordStudentChecks = new List<HealthRecordStudentCheck>();

                var student = _studentRepository.GetByIdAsync(healthrecordList.StudentId).Result;
                var staff = _staffRepository.GetByIdAsync(healthrecordList.StaffId).Result;
                var healthCategory = _healthCategoryRepo.GetByIdAsync(healthrecordList.HealthCategoryId).Result;
                var vaccinationRecords = _ivaccinationRecordRepository.GetRecordsByStudentAsync(healthrecordList.StudentId).Result;
                var healthChecks = _healthCheckRepository.GetHealthChecksByStudentIdAsync(healthrecordList.StudentId).Result;
                HealthRecordStudentCheck check = new HealthRecordStudentCheck
                {
                    StudentName = student.Fullname,
                    HealthCategory = healthCategory.CategoryName,
                    HealthRecordDate = healthrecordList.HealthRecordDate,
                    Healthrecordtitle = healthrecordList.HealthRecordTitle,
                    Healthrecorddescription = healthrecordList.HealthRecordDescription ?? string.Empty,
                    StaffName = staff.Fullname,
                    Status = healthrecordList.Status,
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
                entity.StudentId = healthRecorddto.StudentId;
                entity.HealthCategoryId = healthRecorddto.HealthCategoryId;
                entity.HealthRecordDate = healthRecorddto.HealthRecordDate;
                entity.HealthRecordTitle = healthRecorddto.HealthRecordTitle;
                entity.HealthRecordDescription = healthRecorddto.HealthRecordDescription;
                entity.StaffId = healthRecorddto.StaffId;
                entity.Status = healthRecorddto.Status;
                entity.ModifiedByUserId = healthRecorddto.ModifiedByUserId;
                entity.ModifiedAt = DateTime.Now;
                _healthRecordRepository.Update(entity);
                _healthRecordRepository.Save();
            }
        }

    }
}
