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
                _healthRecordRepository.Update(entity);
                _healthRecordRepository.Save();
            }
        }

        public async Task<List<HealthRecordDto>> GetAllHealthRecordsAsync()
        {
            var healthRecords = _mapper.Map<List<HealthRecordDto>>(await _healthRecordRepository.GetAllAsync());
            return healthRecords;
        }

        public async Task<HealthRecordDto> GetHealthRecordByIdAsync(int id)
        {
            HealthRecordDto healthRecord = _mapper.Map<HealthRecordDto>(await _healthRecordRepository.GetByIdAsync(id));
            return healthRecord;
        }

        public async Task<List<HealthRecordStudentCheck>> GetHealthRecords()
        {
            var healthrecordList = await _healthRecordRepository.GetAllAsync();
            List<HealthRecordStudentCheck> healthRecordStudentChecks = new List<HealthRecordStudentCheck>();
            var student = await _studentRepository.GetAllAsync();
            var staff = await _staffRepository.GetAllAsync();
            var healthCategory = await _healthCategoryRepo.GetAllAsync();
            var vaccinationRecords = await _ivaccinationRecordRepository.GetAllAsync();
            var healthChecks = await _healthCheckRepository.GetAllAsync();

            foreach (var healthRecord in healthrecordList)
            {
                HealthRecordStudentCheck check = new HealthRecordStudentCheck
                {
                    StudentName = student.FirstOrDefault(s => s.Studentid == healthRecord.StudentId)?.Fullname ?? "Không tên",
                    HealthCategory = healthCategory.FirstOrDefault(hc => hc.HealthCategoryId == healthRecord.HealthCategoryId)?.CategoryName ?? "Không xác định",
                    HealthRecordDate = healthRecord.HealthRecordDate,
                    Healthrecordtitle = healthRecord.HealthRecordTitle,
                    Healthrecorddescription = healthRecord.HealthRecordDescription ?? string.Empty,
                    StaffName = staff.FirstOrDefault(s => s.Staffid == healthRecord.StaffId)?.Fullname ?? "Không tên nhân viên",
                    Status = healthRecord.Status,
                    VaccinationRecords = vaccinationRecords.FirstOrDefault(vr => vr.StudentId == healthRecord.StudentId) != null
                        ? _mapper.Map<List<VaccinationRecordDTO>>(vaccinationRecords.Where(vr => vr.StudentId == healthRecord.StudentId).ToList())
                        : null, // Check thử trước nếu có null thì trả null, không thì mới mapping
                    HealthChecks = healthChecks.FirstOrDefault(hc => hc.Studentid == healthRecord.StudentId) != null
                        ? _mapper.Map<List<HealthCheckDTO>>(healthChecks.Where(hc => hc.Studentid == healthRecord.StudentId).ToList())
                        : null
                };
                healthRecordStudentChecks.Add(check);
            }
            return healthRecordStudentChecks;
        }

        public Task<List<HealthRecord>> GetHealthRecordsByStudentIdAsync(int studentId)
        {
            var healthRecords = _healthRecordRepository.GetAllAsync().Result;
                var news = healthRecords.Where(c => c.StudentId == studentId)
                .ToList(); // Convert IEnumerable to List explicitly
            return Task.FromResult(news); // Wrap the result in a Task
        }

        public async Task<HealthRecordStudentCheck> GetHealthRecordsByStudentIdWithCheckAsync(int studentId)
        {
            var List = await _healthRecordRepository.GetAllAsync();
            var healthrecordList = List.FirstOrDefault(h => h.StudentId == studentId);
            var student = await _studentRepository.GetAllAsync();
            var staff = await _staffRepository.GetAllAsync();
            var healthCategory = await _healthCategoryRepo.GetAllAsync();
            var vaccinationRecords = await _ivaccinationRecordRepository.GetAllAsync();
            var healthChecks = await _healthCheckRepository.GetAllAsync();

                HealthRecordStudentCheck check = new HealthRecordStudentCheck
                {
                    StudentName = student.FirstOrDefault(s => s.Studentid == healthrecordList.StudentId)?.Fullname ?? "Không tên",
                    HealthCategory = healthCategory.FirstOrDefault(hc => hc.HealthCategoryId == healthrecordList.HealthCategoryId)?.CategoryName ?? "Không xác định",
                    HealthRecordDate = healthrecordList.HealthRecordDate,
                    Healthrecordtitle = healthrecordList.HealthRecordTitle,
                    Healthrecorddescription = healthrecordList.HealthRecordDescription ?? string.Empty,
                    StaffName = staff.FirstOrDefault(s => s.Staffid == healthrecordList.StaffId)?.Fullname ?? "Không tên nhân viên",
                    Status = healthrecordList.Status,
                    VaccinationRecords = vaccinationRecords.FirstOrDefault(vr => vr.StudentId == healthrecordList.StudentId) != null
                        ? _mapper.Map<List<VaccinationRecordDTO>>(vaccinationRecords.Where(vr => vr.StudentId == healthrecordList.StudentId).ToList())
                        : null, // Check thử trước nếu có null thì trả null, không thì mới mapping
                    HealthChecks = healthChecks.FirstOrDefault(hc => hc.Studentid == healthrecordList.StudentId) != null
                        ? _mapper.Map<List<HealthCheckDTO>>(healthChecks.Where(hc => hc.Studentid == healthrecordList.StudentId).ToList())
                        : null
                };
            return (check);
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
