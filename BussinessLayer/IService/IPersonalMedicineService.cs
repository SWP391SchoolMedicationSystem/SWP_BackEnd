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
        Task<PersonalMedicineDTO> GetPersonalmedicineByIdAsync(int id);
        Task AddPersonalmedicineAsync(AddPersonalMedicineDTO Personalmedicine);
        Task UpdatePersonalmedicine(UpdatePersonalMedicineDTO Personalmedicine);
        Task DeletePersonalmedicine(int id);
        Task ApprovePersonalMedicine(ApprovalPersonalMedicineDTO dto);
        Task RejectPersonalMedicine(ApprovalPersonalMedicineDTO dto);

        Task<List<Personalmedicine>> SearchPersonalmedicinesAsync(string searchTerm);
        Task<List<PersonalMedicineDTO>> GetPersonalmedicinesByParentIdAsync(int parentId);
        Task<List<PersonalMedicineDTO>> GetPersonalmedicinesByMedicineIdAsync(int medicineId);
        Task<List<PersonalMedicineDTO>> GetPersonalmedicinesByApprovalAsync();

        Task<List<PersonalMedicineRequestDTO>> GetRequest();

    }
}
