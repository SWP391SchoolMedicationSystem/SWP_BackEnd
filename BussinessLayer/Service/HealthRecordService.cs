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
    public class HealthRecordService(IHealthRecordRepository healthRecordRepository, IMapper mapper) : IHealthRecordService
    {
        public Task AddHealthRecordAsync(HealthRecordDto healthRecorddto)
        {
            if (healthRecorddto != null)
            {
                Healthrecord healthRecord = mapper.Map<Healthrecord>(healthRecorddto);
                healthRecordRepository.AddAsync(healthRecord);
                healthRecordRepository.Save();
            }
            return Task.CompletedTask;
        }

        public void DeleteHealthRecord(int id)
        {
            var entity = healthRecordRepository.GetByIdAsync(id).Result;

            if (entity != null)
            {
                healthRecordRepository.Delete(id);
                healthRecordRepository.Save();
            }
        }

        public async Task<List<Healthrecord>> GetAllHealthRecordsAsync()
        {
            List<Healthrecord> healthRecords = mapper.Map<List<Healthrecord>>(await healthRecordRepository.GetAllAsync());
            return healthRecords;
        }

        public async Task<HealthRecordDto> GetHealthRecordByIdAsync(int id)
        {
            HealthRecordDto healthRecord = mapper.Map<HealthRecordDto>(await healthRecordRepository.GetByIdAsync(id));
            return healthRecord;
        }

        public Task<List<Healthrecord>> GetHealthRecordsByStudentIdAsync(int studentId)
        {
            var healthRecords = healthRecordRepository.GetAllAsync()
                .Result.Where(hr => hr.Studentid == studentId).ToList();
            return Task.FromResult(mapper.Map<List<Healthrecord>>(healthRecords));
        }

        public void UpdateHealthRecord(HealthRecordDto healthRecorddto, int id)
        {
            var entity = healthRecordRepository.GetByIdAsync(id).Result;
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
                healthRecordRepository.Update(entity);
                healthRecordRepository.Save();
            }
        }

    }
}
