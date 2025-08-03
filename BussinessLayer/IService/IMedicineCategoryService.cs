using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.MedicineCategory;

namespace BussinessLayer.IService
{
    public interface IMedicineCategoryService
    {
        Task<List<MedicineCategoryDTO>> GetAllCategoriesAsync();
        Task<MedicineCategoryDTO> GetCategoryByIdAsync(int id);
        Task AddCategory(CreateMedicineCategoryDTO category);
        Task UpdateCategory(UpdateMedicineCategoryDTO category);
    }
}
