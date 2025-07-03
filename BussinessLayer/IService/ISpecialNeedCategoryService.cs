using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.SpecialNeedCategory;

namespace BussinessLayer.IService
{
    public interface ISpecialNeedCategoryService
    {
        Task<List<SpecialNeedCategoryDTO>> GetAllCategoriesAsync();
        Task<SpecialNeedCategoryDTO> GetCategoryByIdAsync(int id);
        void AddCategoryAsync(CreateSpecialNeedCategoryDTO categoryDto);
        void UpdateCategoryAsync(UpdateSpecialNeedCategoryDTO categoryDto);
        void DeleteCategoryAsync(int id);
    }
}
