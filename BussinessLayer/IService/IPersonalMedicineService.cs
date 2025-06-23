using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IPersonalMedicineService
    {
        Task<List<Personalmedicine>> GetAllPersonalMedicines();
        Task<Personalmedicine> GetPersonalMedicineById(int id);
        Task AddPersonalMedicineAsync(PersonalMedicineDTO medicineDto);
        void UpdatePersonalMedicine(PersonalMedicineDTO medicineDto);

        void DeletePersonalMedicine(int id);

    }
}
