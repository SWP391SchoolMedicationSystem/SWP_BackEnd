using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.HealthRecords;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IHealthRecordService
    {
        Task<List<HealthRecordDto>> GetAllHealthRecordsAsync();
        Task<HealthRecordDto> GetHealthRecordByIdAsync(int id);
        Task AddHealthRecordAsync(CreateHealthRecordDTO healthRecorddto);
        void UpdateHealthRecord(UpdateHealthRecordDTO healthRecorddto, int id);
        void DeleteHealthRecord(int id);
        Task<List<HealthRecordStudentCheck>> GetHealthRecords();
        Task<HealthRecordStudentCheck> GetHealthRecordsByStudentIdWithCheckAsync(int studentId);
        Task<List<HealthRecord>> GetHealthRecordsByStudentIdAsync(int studentId); // Added missing method  
    }
}
