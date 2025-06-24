using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Utils.Configurations;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.HealthRecords;
using DataAccessLayer.DTO.PersonalMedicine;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;

namespace BussinessLayer.Service
{
    public class PersonalMedicineService : IPersonalMedicineService
    {
        private readonly IPersonalMedicineRepository _personalMedicineRepository;
        private readonly IMapper _mapper;
        public PersonalMedicineService(IPersonalMedicineRepository personalMedicineRepository, IMapper mapper)
        {
            _personalMedicineRepository = personalMedicineRepository;
            _mapper = mapper;
        }

        public void AddPersonalMedicine(AddPersonalMedicineDTO personalMedicineDto)
        {
            Personalmedicine pm = _mapper.Map<Personalmedicine>(personalMedicineDto);
            _personalMedicineRepository.Add(pm);
            _personalMedicineRepository.Save();
        }

        public void DeletePersonalMedicine(int id)
        {
            Personalmedicine personalMedicine = _personalMedicineRepository.GetByIdAsync(id).Result;
            if (personalMedicine != null)
            {
                personalMedicine.Isdeleted = true;
                _personalMedicineRepository.Update(personalMedicine);
                _personalMedicineRepository.Save();
            }
        }

        public async Task<List<Personalmedicine>> GetAllPersonalMedicinesAsync()
        {
            List<Personalmedicine> lists = await _personalMedicineRepository.GetAllAsync();
            return lists;
        }

        public async Task<List<Personalmedicine>> GetAvailablePersonalMedicineAsync()
        {
            List<Personalmedicine> lists = await _personalMedicineRepository.GetAllAsync();
            lists = lists.Where(pm => !pm.Isdeleted).ToList();
            return lists;
        }

        public async Task<Personalmedicine> GetPersonalMedicineById(int id)
        {
            var pm = await _personalMedicineRepository.GetByIdAsync(id);
            if (pm == null)
            {
                throw new KeyNotFoundException($"Personal medicine with ID {id} not found.");
            }
            return pm;
        }

        public void UpdatePersonalMedicineAsync(UpdatePersonalMedicineDTO personalMedicineDto)
        {
            var personalMedicine = _personalMedicineRepository.GetByIdAsync(personalMedicineDto.Personalmedicineid).Result;
            if (personalMedicine == null)
            {
                throw new KeyNotFoundException($"Personal medicine with ID {personalMedicineDto.Personalmedicineid} not found.");
            }
            personalMedicine.Studentid = personalMedicineDto.Studentid;
            personalMedicine.Medicinename = personalMedicineDto.Medicinename;
            personalMedicine.Quanttiy = personalMedicineDto.Quanttiy;
            personalMedicine.Receivedate = personalMedicineDto.Receivedate; //can change to keep the original date
            personalMedicine.Expirydate = personalMedicineDto.Expirydate;
            personalMedicine.Staffid = personalMedicineDto.Staffid;
            personalMedicine.Isdeleted = personalMedicineDto.Isdeleted;
            personalMedicine.Modifiedby = personalMedicineDto.Modifiedby;
            personalMedicine.Modifieddate = DateTime.Now;
            _personalMedicineRepository.Update(personalMedicine);
            _personalMedicineRepository.Save();
        }
    }
}
