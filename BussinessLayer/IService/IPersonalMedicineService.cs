using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.PersonalMedicine;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IPersonalmedicineService
    {

        Task<List<PersonalMedicineDTO>> GetAllPersonalmedicinesAsync();
        Task<Personalmedicine> GetPersonalmedicineByIdAsync(int id);
        Task AddPersonalmedicineAsync(AddPersonalMedicineDTO Personalmedicine);
        void UpdatePersonalmedicine(UpdatePersonalMedicineDTO Personalmedicine);
        void DeletePersonalmedicine(int id);
        Task<List<Personalmedicine>> SearchPersonalmedicinesAsync(string searchTerm);
        Task<List<Personalmedicine>> GetPersonalmedicinesByParentIdAsync(int parentId);
        Task<List<Personalmedicine>> GetPersonalmedicinesByMedicineIdAsync(int medicineId);
        Task<List<Personalmedicine>> GetPersonalmedicinesByApprovalAsync(int isApproved);

        Task<List<PersonalMedicineRequestDTO>> GetRequest();

    }
}
