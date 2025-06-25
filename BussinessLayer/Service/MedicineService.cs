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
            throw new NotImplementedException();
        }

        public void DeleteMedicine(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Medicine>> GetAllMedicinesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Medicine> GetMedicineByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateMedicine(UpdateMedicineDTO medicine)
        {
            throw new NotImplementedException();
        }
    }
}
