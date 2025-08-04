using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO.SpecialNeedCategory;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class SpecialNeedCategoryService : ISpecialNeedCategoryService
    {
        private readonly IStudentSpecialNeedCategoryRepo _specialNeedCategoryRepository;
        private readonly IMapper _mapper;
        public SpecialNeedCategoryService(IStudentSpecialNeedCategoryRepo specialNeedCategoryRepository, IMapper mapper)
        {
            _specialNeedCategoryRepository = specialNeedCategoryRepository;
            _mapper = mapper;
        }
        public async Task<List<SpecialNeedCategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _specialNeedCategoryRepository.GetAllAsync();
            return _mapper.Map<List<SpecialNeedCategoryDTO>>(categories);
        }
        public async Task<SpecialNeedCategoryDTO> GetCategoryByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid category ID.", nameof(id));
            }
            var category = await _specialNeedCategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException("Special need category not found.");
            }
            return _mapper.Map<SpecialNeedCategoryDTO>(category);
        }
        public async Task AddCategoryAsync(CreateSpecialNeedCategoryDTO category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null.");
            }
            var newCategory = _mapper.Map<SpecialNeedsCategory>(category);
            await _specialNeedCategoryRepository.AddAsync(newCategory);
            await _specialNeedCategoryRepository.SaveChangesAsync();
        }
        public async Task UpdateCategoryAsync(UpdateSpecialNeedCategoryDTO category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null.");
            }
            var existingCategory = await _specialNeedCategoryRepository.GetByIdAsync(category.SpecialNeedCategoryId);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException("Special need category not found.");
            }
            existingCategory.CategoryName = category.CategoryName;
            existingCategory.Description = category.Description;
            existingCategory.Isdeleted = category.IsDelete;
            _specialNeedCategoryRepository.Update(existingCategory);
            _specialNeedCategoryRepository.Save();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid category ID.", nameof(id));
            }
            var category = await _specialNeedCategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException("Special need category not found.");
            }
            category.Isdeleted = true;
            _specialNeedCategoryRepository.Update(category);
            _specialNeedCategoryRepository.Save();
        }
    }
}
