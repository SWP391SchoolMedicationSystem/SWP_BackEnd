using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Utils.Configurations;
using DataAccessLayer.DTO;
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
        private readonly IMapper _mapper;
        private readonly AppSetting _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HealthRecordService(IHealthRecordRepository healthRecordRepository, IMapper mapper,
            IOptionsMonitor<AppSetting> option, IHttpContextAccessor httpContextAccessor)
        {
            _healthRecordRepository = healthRecordRepository;
            _mapper = mapper;
            _appSettings = option.CurrentValue;
            _httpContextAccessor = httpContextAccessor;
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
                _healthRecordRepository.Delete(id);
                _healthRecordRepository.Save();
            }
        }

        public async Task<List<Healthrecord>> GetAllHealthRecordsAsync()
        {
            List<Healthrecord> healthRecords = _mapper.Map<List<Healthrecord>>(await _healthRecordRepository.GetAllAsync());
            return healthRecords;
        }

        public async Task<HealthRecordDTO> GetHealthRecordByIdAsync(int id)
        {
            HealthRecordDTO healthRecord = _mapper.Map<HealthRecordDTO>(await _healthRecordRepository.GetByIdAsync(id));
            return healthRecord;
        }

        public Task<List<Healthrecord>> GetHealthRecordsByStudentIdAsync(int studentId)
        {
            var healthRecords = _healthRecordRepository.GetAllAsync()
                .Result.Where(hr => hr.Studentid == studentId).ToList();
            return Task.FromResult(_mapper.Map<List<Healthrecord>>(healthRecords));
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
