using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO.Medicines;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly IMapper _mapper;
        public MedicineService(IMedicineRepository medicineRepository, IMapper mapper)
        {
            _medicineRepository = medicineRepository;
            _mapper = mapper;
        }
        public void AddMedicine(CreateMedicineDTO medicine)
        {
            MedicineCatalog entity = _mapper.Map<MedicineCatalog>(medicine);
            entity.IsDeleted = false;
            _medicineRepository.AddAsync(entity);
            _medicineRepository.Save();
        }

        public void DeleteMedicine(int id)
        {
            var medicine = _medicineRepository.GetByIdAsync(id).Result;
            if (medicine != null)
            {
                medicine.IsDeleted = true;
                _medicineRepository.Update(medicine);
                _medicineRepository.Save();
            }
        }

        public async Task<List<MedicineDTO>> GetAllMedicinesAsync()
        {
            var medicines = await _medicineRepository.GetAllAsync();
            var dtos = _mapper.Map<List<MedicineDTO>>(medicines);
            return dtos;
        }

        public async Task<List<MedicineDTO>> GetAvailableMedicinesAsync()
        {
            var allMedicines = await _medicineRepository.GetAllAsync();
            var medicines = allMedicines.Where(m => !m.IsDeleted).ToList();
            var lists = _mapper.Map<List<MedicineDTO>>(medicines);  
            return lists;
        }

        public async Task<MedicineDTO> GetMedicineByIdAsync(int id)
        {
            var medicines = await _medicineRepository.GetByIdAsync(id);
            var dto = _mapper.Map<MedicineDTO>(medicines);
            return dto;
        }

        public async Task<List<MedicineDTO>> GetMedicinesByCategoryIdAsync(int categoryId)
        {
            var medicines = await _medicineRepository.GetAllAsync();
            var filtered = medicines.Where(m => m.MedicineCategoryId == categoryId).ToList();
            return _mapper.Map<List<MedicineDTO>>(filtered);
        }

        public async Task<List<MedicineDTO>> SearchMedicinesNameAsync(string searchTerm)
        {
            var medicines = await _medicineRepository.GetAllAsync();
            var filtered = medicines
                .Where(m => m.MedicineName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return _mapper.Map<List<MedicineDTO>>(filtered);
        }

        public void UpdateMedicine(UpdateMedicineDTO medicine)
        {
            if (medicine != null)
            {
                var entity = _medicineRepository.GetByIdAsync(medicine.MedicineId).Result;
                if (entity != null)
                {
                    entity.MedicineName = medicine.MedicineName;
                    entity.MedicineCategoryId = medicine.MedicineCategoryId;
                    entity.DefaultDosage = medicine.DefaultDosage;
                    entity.SideEffects = medicine.SideEffects;
                    entity.Usage = medicine.Usage;
                    entity.mo = DateTime.Now;
                    entity.Updatedby = medicine.Updatedby;
                    _medicineRepository.Update(entity);
                    _medicineRepository.Save();
                }
            }
        }
    }
}
