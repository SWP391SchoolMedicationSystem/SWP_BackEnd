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
            Medicine entity = _mapper.Map<Medicine>(medicine);
            _medicineRepository.AddAsync(entity);
            _medicineRepository.Save();
        }

        public async Task<List<MedicineDTO>> GetAllMedicinesAsync()
        {
            var medicines = await _medicineRepository.GetAllAsync();
            var dtos = _mapper.Map<List<MedicineDTO>>(medicines);
            return dtos;
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

        public void UpdateMedicine(UpdateMedicineDTO medicine)
        {
            if (medicine != null)
            {
                var entity = _medicineRepository.GetByIdAsync(medicine.Medicineid).Result;
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
