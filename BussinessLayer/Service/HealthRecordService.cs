using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Utils.Configurations;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.HealthChecks;
using DataAccessLayer.DTO.HealthRecords;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
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
        private readonly IParentRepository _parentRepository;

        public HealthRecordService(
            IParentRepository parentRepository,
            IHealthRecordRepository healthRecordRepository,
            IVaccinationRecordRepository vaccinationRecordRepo,
            IHealthCheckRepo healthCheckRepository,
            IStudentRepo studentRepository,
            IHealthRecordCategoryRepo healthCategoryRepo,
            IStaffRepository staffRepository,
            IMapper mapper)
        {
            _parentRepository = parentRepository;
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
            var students = await _studentRepository.GetAllAsync();
            var staffs = await _staffRepository.GetAllAsync();
            var healthCategories = await _healthCategoryRepo.GetAllAsync();
            var parent = await _parentRepository.GetAllAsync();
            return healthRecords
                .Select(hr => new HealthRecordDto
                {
                    healthrecordid = hr.healthrecordid,
                    studentid = hr.studentid,
                    StudentName = students.FirstOrDefault(s => s.Studentid == hr.studentid)?.Fullname ?? "Không tên",
                    parentID = students.FirstOrDefault(s => s.Studentid == hr.studentid)?.Parentid ?? 0,
                    ParentName = parent.FirstOrDefault(p => p.Parentid == students.FirstOrDefault(s => s.Studentid == hr.studentid)?.Parentid)?.Fullname ?? "Không tên phụ huynh",
                    healthcategoryid = hr.healthcategoryid,
                    HealthCategoryName = healthCategories.FirstOrDefault(hc => hc.HealthCategoryId == hr.healthcategoryid)?.CategoryName ?? "Không xác định",
                    healthrecorddate = hr.healthrecorddate,
                    healthrecordtitle = hr.healthrecordtitle,
                    healthrecorddescription = hr.healthrecorddescription ?? string.Empty,
                    staffID = hr.staffID,
                    staffName = staffs.FirstOrDefault(s => s.Staffid == hr.staffID)?.Fullname ?? "Không tên nhân viên",
                    Status = hr.Status,
                    CreatedAt = hr.CreatedAt,
                    ModifiedAt = hr.ModifiedAt,
                    CreatedByUserId = hr.CreatedByUserId,
                    ModifiedByUserId = hr.ModifiedByUserId
                })
                .ToList();
        }

        public async Task<HealthRecordDto> GetHealthRecordByIdAsync(int id)
        {
            return _mapper.Map<HealthRecordDto>(await _healthRecordRepository.GetByIdAsync(id));
        }

        public async Task<List<HealthRecordStudentCheck>> GetHealthRecords()
        {
            var healthrecordList = await _healthRecordRepository.GetAllAsync();
            var student = await _studentRepository.GetAllAsync();
            var staff = await _staffRepository.GetAllAsync();
            var healthCategory = await _healthCategoryRepo.GetAllAsync();
            var vaccinationRecords = await _ivaccinationRecordRepository.GetAllAsync();
            var healthChecks = await _healthCheckRepository.GetAllAsync();
            var parent = await _parentRepository.GetAllAsync();
            return healthrecordList.Select(healthRecord => new HealthRecordStudentCheck
            {
                StudentName = student.FirstOrDefault(s => s.Studentid == healthRecord.StudentId)?.Fullname ?? "Không tên",
                ParentName = parent.FirstOrDefault(p => p.Parentid == student.FirstOrDefault(s => s.Studentid == healthRecord.StudentId)?.Parentid)?.Fullname ?? "Không tên phụ huynh",
                HealthCategory = healthCategory.FirstOrDefault(hc => hc.HealthCategoryId == healthRecord.HealthCategoryId)?.CategoryName ?? "Không xác định",
                HealthRecordDate = healthRecord.HealthRecordDate,
                Healthrecordtitle = healthRecord.HealthRecordTitle,
                Healthrecorddescription = healthRecord.HealthRecordDescription ?? string.Empty,
                staffName = staff.FirstOrDefault(s => s.Staffid == healthRecord.StaffId)?.Fullname ?? "Không tên nhân viên",
                Status = healthRecord.Status,
                VaccinationRecords = vaccinationRecords.Where(vr => vr.StudentId == healthRecord.StudentId).Select(vr => _mapper.Map<VaccinationRecordDTO>(vr)).ToList(),
                HealthChecks = healthChecks.Where(hc => hc.Studentid == healthRecord.StudentId).Select(hc => _mapper.Map<HealthCheckDTO>(hc)).ToList()
            }).ToList();
        }

        public Task<List<HealthRecord>> GetHealthRecordsByStudentIdAsync(int studentId)
        {
            var healthRecords = _healthRecordRepository.GetAllAsync().Result;
            var news = healthRecords.Where(c => c.StudentId == studentId).ToList();
            return Task.FromResult(news);
        }

        public async Task<HealthRecordStudentCheck> GetHealthRecordsByStudentIdWithCheckAsync(int studentId)
        {
            var healthrecordList = (await _healthRecordRepository.GetAllAsync()).FirstOrDefault(h => h.StudentId == studentId);
            if (healthrecordList == null) throw new ArgumentNullException(nameof(healthrecordList));

            var student = await _studentRepository.GetAllAsync();
            var staff = await _staffRepository.GetAllAsync();
            var healthCategory = await _healthCategoryRepo.GetAllAsync();
            var vaccinationRecords = await _ivaccinationRecordRepository.GetAllAsync();
            var healthChecks = await _healthCheckRepository.GetAllAsync();

            return new HealthRecordStudentCheck
            {
                StudentName = student.FirstOrDefault(s => s.Studentid == healthrecordList.StudentId)?.Fullname ?? "Không tên",
                HealthCategory = healthCategory.FirstOrDefault(hc => hc.HealthCategoryId == healthrecordList.HealthCategoryId)?.CategoryName ?? "Không xác định",
                HealthRecordDate = healthrecordList.HealthRecordDate,
                Healthrecordtitle = healthrecordList.HealthRecordTitle,
                Healthrecorddescription = healthrecordList.HealthRecordDescription ?? string.Empty,
                staffName = staff.FirstOrDefault(s => s.Staffid == healthrecordList.StaffId)?.Fullname ?? "Không tên nhân viên",
                Status = healthrecordList.Status,
                VaccinationRecords = vaccinationRecords.Where(vr => vr.StudentId == healthrecordList.StudentId).Select(vr => _mapper.Map<VaccinationRecordDTO>(vr)).ToList(),
                HealthChecks = healthChecks.Where(hc => hc.Studentid == healthrecordList.StudentId).Select(hc => _mapper.Map<HealthCheckDTO>(hc)).ToList()
            };
        }

        public void UpdateHealthRecord(UpdateHealthRecordDTO healthRecorddto, int id)
        {
            var entity = _healthRecordRepository.GetByIdAsync(id).Result;
            if (entity != null)
            {
                entity.StudentId = healthRecorddto.studentid;
                entity.HealthCategoryId = healthRecorddto.healthcategoryid;
                entity.HealthRecordDate = healthRecorddto.healthrecorddate;
                entity.HealthRecordTitle = healthRecorddto.healthrecordtitle;
                entity.HealthRecordDescription = healthRecorddto.healthrecorddescription;
                entity.StaffId = healthRecorddto.staffid;
                entity.Status = healthRecorddto.Status;
                entity.ModifiedByUserId = healthRecorddto.ModifiedByUserId;
                entity.ModifiedAt = DateTime.Now;
                _healthRecordRepository.Update(entity);
                _healthRecordRepository.Save();
            }
        }
    }
}
