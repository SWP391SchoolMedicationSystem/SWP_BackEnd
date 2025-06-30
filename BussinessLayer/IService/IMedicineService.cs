using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.Medicines;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IMedicineService
    {
        Task<List<MedicineDTO>> GetAllMedicinesAsync();
        Task<MedicineDTO> GetMedicineByIdAsync(int id);
        void AddMedicine(CreateMedicineDTO medicine);
        void UpdateMedicine(UpdateMedicineDTO medicine);
        Task<List<MedicineDTO>> SearchMedicinesNameAsync(string searchTerm);
        Task<List<MedicineDTO>> GetMedicinesByCategoryIdAsync(int categoryId);
        void DeleteMedicine(int id);
        Task<List<MedicineDTO>> GetAvailableMedicinesAsync();
    }
}
