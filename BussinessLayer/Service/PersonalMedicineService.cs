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
        IMedicineRepository medicineRepository, IStudentRepo studentRepo, IClassRoomRepository classRoomRepository,
        IMedicineScheduleRepository medicineScheduleRepository, IMedicineCategoryRepository medicineCategoryRepository,
        IScheduleDetailRepo scheduleDetailRepo, IMapper mapper) : IPersonalmedicineService
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
            var dtolist = mapper.Map<List<PersonalMedicineDTO>>(personalMedicines);
            foreach (var item in dtolist)
            {
                item.Phone = parentRepository.GetByIdAsync(item.Parentid).Result?.Phone ?? "No phone number";
            }
            return dtolist;
        }

        public async Task<PersonalMedicineDTO> GetPersonalmedicineByIdAsync(int id)
        {
            var personalMedicines = await PersonalmedicineRepository.GetByIdAsync(id);
            var dto = mapper.Map<PersonalMedicineDTO>(personalMedicines);
            dto.Phone = parentRepository.GetByIdAsync(dto.Parentid).Result?.Phone ?? "No phone number";
            return dto;
        }

        public Task<List<PersonalMedicineDTO>> GetPersonalmedicinesByMedicineIdAsync(int medicineId)
        {
            var personalMedicines = PersonalmedicineRepository.GetAllAsync().Result;
            var mappedMedicines = mapper.Map<List<PersonalMedicineDTO>>(personalMedicines);

            return Task.FromResult(mappedMedicines.Where(md => md.Medicineid == medicineId).ToList());
        }

        public Task<List<PersonalMedicineDTO>> GetPersonalmedicinesByParentIdAsync(int parentId)
        {
            var personalMedicines = PersonalmedicineRepository.GetAllAsync().Result;
            var mappedMedicines = mapper.Map<List<PersonalMedicineDTO>>(personalMedicines);

            return Task.FromResult(mappedMedicines.Where(md => md.Parentid == parentId).ToList());
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
            var PersonalmedicineEntity = PersonalmedicineRepository.GetByIdAsync(Personalmedicine.personalMedicineId).Result;
            if (PersonalmedicineEntity == null)
            {
                throw new KeyNotFoundException("Medicine donation not found.");
            }
            else
            {
                PersonalmedicineEntity.Medicineid = Personalmedicine.Medicineid;
                PersonalmedicineEntity.Studentid = Personalmedicine.Studentid;
                PersonalmedicineEntity.Parentid = Personalmedicine.Parentid;
                PersonalmedicineEntity.Quantity = Personalmedicine.Quantity;
                PersonalmedicineEntity.Receiveddate = Personalmedicine.Receiveddate;
                PersonalmedicineEntity.ExpiryDate = Personalmedicine.ExpiryDate;
                PersonalmedicineEntity.Status = Personalmedicine.Status;
                if (PersonalmedicineEntity.Status)
                {
                    PersonalmedicineEntity.Isapproved = true;
                }
                PersonalmedicineEntity.Note = Personalmedicine.Note;
                PersonalmedicineEntity.Modifieddate = DateTime.Now;
                PersonalmedicineRepository.Update(PersonalmedicineEntity);
                PersonalmedicineRepository.Save();

            }


        }
        public Task<List<PersonalMedicineDTO>> GetPersonalmedicinesByApprovalAsync()
        {
            var Personalmedicines = PersonalmedicineRepository.GetAllAsync();
            var dto = mapper.Map<List<PersonalMedicineDTO>>(Personalmedicines.Result.Where(md => md.Isapproved == true && md.Status == true));
            return Task.FromResult(dto);
        }

        public async Task<List<PersonalMedicineRequestDTO>> GetRequest()
        {
            var personalMedicinesList = await PersonalmedicineRepository.GetAllAsync();
            List<PersonalMedicineRequestDTO> personalMedicineRequests = new List<PersonalMedicineRequestDTO>();
            var ScheduleDetailList = await scheduleDetailRepo.GetAllAsync();

            foreach (var personalMedicine in personalMedicinesList)
            {
                if (!personalMedicine.Isdeleted)
                {
                    List<Scheduledetail> scheduledetails = new List<Scheduledetail>();
                    Classroom classRoom = classRoomRepository.GetAllAsync().Result.FirstOrDefault(c => c.Classid == personalMedicine.Student.Classid);
                    var studentname = personalMedicine.Student.Fullname;
                    var medicine = personalMedicine.Medicine;
                    var type = medicineCategoryRepository.GetByIdAsync(medicine.Medicinecategoryid).Result?.Medicinecategoryname ?? "Unknown";
                    var MedicineScheduleList = medicineScheduleRepository.GetAllAsync().Result.Where(ms => ms.Personalmedicineid == personalMedicine.Personalmedicineid).ToList();
                    foreach (var schedule in MedicineScheduleList)
                    {
                        scheduledetails.Add(ScheduleDetailList.FirstOrDefault(sd => sd.Scheduledetailid == schedule.Scheduledetails));
                    }
                    var scheduling = mapper.Map<List<ScheduleDetailDTO>>(scheduledetails);
                    var request = new PersonalMedicineRequestDTO
                    {
                        Studentid = personalMedicine.Studentid,
                        StudentName = studentname,
                        ParentId = personalMedicine.Parentid,
                        ParentName = personalMedicine.Parent.Fullname,
                        ClassName = classRoom.Classname,
                        MedicineName = medicine.Medicinename,
                        MedicineType = type,
                        Quantity = personalMedicine.Quantity,
                        ExpiryDate = personalMedicine.ExpiryDate.HasValue ? DateOnly.FromDateTime(personalMedicine.ExpiryDate.Value) : DateOnly.MinValue,
                        Note = personalMedicine.Note ?? string.Empty,
                        PhoneNumber = personalMedicine.Parent.Phone,
                        PreferedTime = scheduling,
                        isApproved = personalMedicine.Isapproved,
                        CreatedDate = personalMedicine.Createddate
                    };
                    personalMedicineRequests.Add(request);
                }
            }
            var task = Task.FromResult(personalMedicineRequests);
            return await task;

        }
        public Task ApprovePersonalMedicine(ApprovalPersonalMedicineDTO dto)
        {
            var personalMedicine = PersonalmedicineRepository.GetByIdAsync(dto.Personalmedicineid).Result;
            if (personalMedicine != null)
            {
                personalMedicine.Status = true;
                personalMedicine.Approvedby = dto.Approvedby;
                personalMedicine.Isapproved = true;
                personalMedicine.Modifieddate = DateTime.Now;
                PersonalmedicineRepository.Update(personalMedicine);
                PersonalmedicineRepository.Save();
            }
            return Task.CompletedTask;
        }
        public Task RejectPersonalMedicine(ApprovalPersonalMedicineDTO dto)
        {
            var personalMedicine = PersonalmedicineRepository.GetByIdAsync(dto.Personalmedicineid).Result;
            if (personalMedicine != null)
            {
                personalMedicine.Status = true;
                personalMedicine.Approvedby = dto.Approvedby;
                personalMedicine.Isapproved = false;
                personalMedicine.Modifieddate = DateTime.Now;
                PersonalmedicineRepository.Update(personalMedicine);
                PersonalmedicineRepository.Save();

            }
            return Task.CompletedTask;
        }

    }
}
