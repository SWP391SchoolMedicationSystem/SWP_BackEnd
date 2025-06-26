using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IHealthRecordService
    {
        Task<List<Healthrecord>> GetAllHealthRecordsAsync();
        Task<HealthRecordDto> GetHealthRecordByIdAsync(int id);
        Task AddHealthRecordAsync(HealthRecordDto healthRecorddto);
        void UpdateHealthRecord(HealthRecordDto healthRecorddto, int id);
        void DeleteHealthRecord(int id);
        Task<List<Healthrecord>> GetHealthRecordsByStudentIdAsync(int studentId);
    }
}
