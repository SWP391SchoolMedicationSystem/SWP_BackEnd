using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO.MedicineCategory;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class MedicineCategoryService : IMedicineCategoryService
    {
        private readonly IMedicineCategoryRepository _medicineCategoryRepository;
        private readonly IMapper _mapper;
        public MedicineCategoryService(IMedicineCategoryRepository medicineCategoryRepository, IMapper mapper)
        {
            _medicineCategoryRepository = medicineCategoryRepository;
            _mapper = mapper;
        }
        public async Task AddCategory(CreateMedicineCategoryDTO category)
        {
            var categoryEntity = _mapper.Map<Medicinecategory>(category);
            _medicineCategoryRepository.Add(categoryEntity);
            _medicineCategoryRepository.Save();
        }

        public async Task<List<MedicineCategoryDTO>> GetAllCategoriesAsync()
        {
            List<Medicinecategory> categories = await _medicineCategoryRepository.GetAllAsync();
            var categoryDTOs = _mapper.Map<List<MedicineCategoryDTO>>(categories);
            return categoryDTOs;
        }

        public async Task<MedicineCategoryDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _medicineCategoryRepository.GetByIdAsync(id);
            var categoryDTO = _mapper.Map<MedicineCategoryDTO>(category);
            return categoryDTO;
        }

        public async Task UpdateCategory(UpdateMedicineCategoryDTO category)
        {
            var categoryEntity = _mapper.Map<Medicinecategory>(category);
            _medicineCategoryRepository.Update(categoryEntity);
            _medicineCategoryRepository.Save();
        }
    }
}
