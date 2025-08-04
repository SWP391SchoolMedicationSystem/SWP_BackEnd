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
        public async Task AddMedicine(CreateMedicineDTO medicine)
        {
            Medicine entity = _mapper.Map<Medicine>(medicine);
            entity.Isdeleted = false;
            await _medicineRepository.AddAsync(entity);
            await _medicineRepository.SaveChangesAsync();
        }

        public async Task DeleteMedicine(int id)
        {
            var medicine = await _medicineRepository.GetByIdAsync(id);
            if (medicine != null)
            {
                medicine.Isdeleted = true;
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
            var medicines = allMedicines.Where(m => !m.Isdeleted.Value).ToList();
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
            var filtered = medicines.Where(m => m.Medicinecategoryid == categoryId).ToList();
            return _mapper.Map<List<MedicineDTO>>(filtered);
        }

        public async Task<List<MedicineDTO>> SearchMedicinesNameAsync(string searchTerm)
        {
            var medicines = await _medicineRepository.GetAllAsync();
            var filtered = medicines
                .Where(m => m.Medicinename.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return _mapper.Map<List<MedicineDTO>>(filtered);
        }

        public async Task UpdateMedicine(UpdateMedicineDTO medicine)
        {
            if (medicine != null)
            {
                var entity = await _medicineRepository.GetByIdAsync(medicine.Medicineid);
                if (entity != null)
                {
                    entity.Medicinename = medicine.Medicinename;
                    entity.Medicinecategoryid = medicine.Medicinecategoryid;
                    entity.Type = medicine.Type;
                    entity.Quantity = medicine.Quantity;
                    entity.Updatedat = DateTime.Now;
                    entity.Updatedby = medicine.Updatedby;
                    _medicineRepository.Update(entity);
                    _medicineRepository.Save();
                }
            }
        }
    }
}
