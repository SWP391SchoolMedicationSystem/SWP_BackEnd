using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IVaccinationRecordService
    {
        Task<List<VaccinationRecordStudentEvent>> GetAllVaccinationRecords();
        Task<StudentVaccinationRecord> GetVaccinationRecordById(int id);
        Task AddVaccinationRecordAsync(VaccinationRecordDTO record);
        void UpdateVaccinationRecord(VaccinationRecordDTO record, int id);
        void DeleteVaccinationRecord(int id);

    }
}
