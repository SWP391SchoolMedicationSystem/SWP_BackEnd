using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.PersonalMedicine;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using NPOI.OpenXmlFormats.Dml;

namespace BussinessLayer.Service
{
    public class PersonalmedicineService(IPersonalMedicineRepository PersonalmedicineRepository, IParentRepository parentRepository,
        IMedicineRepository medicineRepository,IStudentRepo studentRepo, IMapper mapper) : IPersonalmedicineService
    {
        public Task AddPersonalmedicineAsync(AddPersonalMedicineDTO Personalmedicine)
        {
            var PersonalmedicineEntity = mapper.Map<Personalmedicine>(Personalmedicine);
            PersonalmedicineEntity.Medicine = medicineRepository.GetByIdAsync(Personalmedicine.Medicineid).Result;
            
            PersonalmedicineEntity.Parent = parentRepository.GetByIdAsync(Personalmedicine.Parentid.Value).Result;
            if (PersonalmedicineEntity.Parent == null)
            {
                return Task.FromException(new KeyNotFoundException("Parent not found."));
            }
            PersonalmedicineEntity.Student = studentRepo.GetByIdAsync(Personalmedicine.Studentid.Value).Result;
            if (PersonalmedicineEntity.Student == null)
            {
                return Task.FromException(new KeyNotFoundException("Student not found."));
            }

            PersonalmedicineEntity.Status = false;
            PersonalmedicineEntity.Createddate = DateTime.Now;
            PersonalmedicineEntity.Medicine = medicineRepository.GetByIdAsync(Personalmedicine.Medicineid).Result;
            if (PersonalmedicineEntity.Medicine == null)
            {
                return Task.FromException(new KeyNotFoundException("Medicine not found."));
            }
            PersonalmedicineRepository.AddAsync(PersonalmedicineEntity);
            PersonalmedicineRepository.Save();
            return Task.CompletedTask;
        }

        public void DeletePersonalmedicine(int id)
        {
            var Personalmedicine = PersonalmedicineRepository.GetByIdAsync(id).Result;
            if (Personalmedicine != null)
            {
                Personalmedicine.Isdeleted = true;
                Personalmedicine.Modifieddate = DateTime.Now;
                PersonalmedicineRepository.Update(Personalmedicine);
                PersonalmedicineRepository.Save();
            }
            else
            {
                throw new KeyNotFoundException("Medicine donation not found.");
            }
        }

        public async Task<List<PersonalMedicineDTO>> GetAllPersonalmedicinesAsync()
        {
            var personalMedicines = await PersonalmedicineRepository.GetAllAsync();
            return mapper.Map<List<PersonalMedicineDTO>>(personalMedicines);
        }

        public Task<Personalmedicine> GetPersonalmedicineByIdAsync(int id)
        {
            return PersonalmedicineRepository.GetByIdAsync(id);
        }

        public Task<List<Personalmedicine>> GetPersonalmedicinesByMedicineIdAsync(int medicineId)
        {
            var Personalmedicines = PersonalmedicineRepository.GetAllAsync();
            return Personalmedicines.ContinueWith(task =>
            {
                return task.Result.Where(md => md.Medicineid == medicineId).ToList();
            });
        }

        public Task<List<Personalmedicine>> GetPersonalmedicinesByParentIdAsync(int parentId)
        {
            var Personalmedicines = PersonalmedicineRepository.GetAllAsync();
            return Personalmedicines.ContinueWith(task =>
            {
                return task.Result.Where(md => md.Parentid == parentId).ToList();
            });
        }

        public Task<List<Personalmedicine>> SearchPersonalmedicinesAsync(string searchTerm)
        {
            var Personalmedicines = PersonalmedicineRepository.GetAllAsync();
            return Personalmedicines.ContinueWith(task =>
            {
                return task.Result.Where(donation =>
                    donation.Medicine.Medicinename.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    (donation.Parent != null && donation.Parent.Fullname.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            });
        }

        public void UpdatePersonalmedicine(UpdatePersonalMedicineDTO Personalmedicine)
        {
            var PersonalmedicineEntity = PersonalmedicineRepository.GetByIdAsync(Personalmedicine.Personalmedicineid).Result;
            if (PersonalmedicineEntity == null)
            {
                throw new KeyNotFoundException("Medicine donation not found.");
            }
            else
            {
                PersonalmedicineEntity.Modifieddate = DateTime.Now;
                medicineRepository.Update(PersonalmedicineEntity.Medicine);

            }


        }
        public Task<List<Personalmedicine>> GetPersonalmedicinesByApprovalAsync(int isApproved)
        {
            var Personalmedicines = PersonalmedicineRepository.GetAllAsync();
            return Personalmedicines.ContinueWith(task =>
            {
                return task.Result.Where(md => md.Isapproved == (isApproved == 1)).ToList();
            });
        }
    }
}
