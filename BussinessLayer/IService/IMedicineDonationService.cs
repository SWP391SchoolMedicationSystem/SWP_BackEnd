using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IMedicineDonationService
    {

        Task<List<Medicinedonation>> GetAllMedicineDonationsAsync();
        Task<Medicinedonation> GetMedicineDonationByIdAsync(int id);
        Task AddMedicineDonationAsync(MedicineDonationDto medicinedonation);
        void UpdateMedicineDonation(int id, MedicineDonationDto medicinedonation);
        void DeleteMedicineDonation(int id);
        Task<List<Medicinedonation>> SearchMedicineDonationsAsync(string searchTerm);
        Task<List<Medicinedonation>> GetMedicineDonationsByParentIdAsync(int parentId);
        Task<List<Medicinedonation>> GetMedicineDonationsByMedicineIdAsync(int medicineId);

    }
}
