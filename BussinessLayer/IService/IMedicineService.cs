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
        Task<List<Medicine>> GetAllMedicinesAsync();
        Task<Medicine> GetMedicineByIdAsync(int id);
        void AddMedicine(CreateMedicineDTO medicine);
        void UpdateMedicine(UpdateMedicineDTO medicine);
        void DeleteMedicine(int id);
    }
}
