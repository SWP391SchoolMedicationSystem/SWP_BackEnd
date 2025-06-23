using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.PersonalMedicine;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IPersonalMedicineService
    {
        public void AddPersonalMedicine(AddPersonalMedicineDTO personalMedicineDto);
        public void DeletePersonalMedicine(int id);
        public Task<List<Personalmedicine>> GetAllPersonalMedicinesAsync();
        public Task<List<Personalmedicine>> GetAvailablePersonalMedicineAsync();
        public Task<Personalmedicine> GetPersonalMedicineById(int id);
        public void UpdatePersonalMedicineAsync(UpdatePersonalMedicineDTO personalMedicineDto);

    }
}
