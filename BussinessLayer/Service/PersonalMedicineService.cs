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
using static BussinessLayer.Utils.Constants;

namespace BussinessLayer.Service
{
    public class PersonalmedicineService(IPersonalMedicineRepository PersonalmedicineRepository, IParentRepository parentRepository,
        IStudentRepo studentRepo, IClassRoomRepository classRoomRepository,
        IMedicineScheduleRepository medicineScheduleRepository, IMedicineCategoryRepository medicineCategoryRepository,
        IScheduleDetailRepo scheduleDetailRepo, IMapper mapper) : IPersonalmedicineService
    {
        public Task AddPersonalmedicineAsync(AddPersonalMedicineDTO Personalmedicine)
        {
            var PersonalmedicineEntity = mapper.Map<Personalmedicine>(Personalmedicine);

            PersonalmedicineEntity.Parent = parentRepository.GetByIdAsync(Personalmedicine.Parentid).Result;
            if (PersonalmedicineEntity.Parent == null)
            {
                return Task.FromException(new KeyNotFoundException("Parent not found."));
            }
            PersonalmedicineEntity.Student = studentRepo.GetByIdAsync(Personalmedicine.Studentid).Result;
            if (PersonalmedicineEntity.Student == null)
            {
                return Task.FromException(new KeyNotFoundException("Student not found."));
            }

            PersonalmedicineEntity.DeliveryStatus = PersonalMedicineStatus.Pending;
            PersonalmedicineEntity.CreatedAt = DateTime.Now;
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
                Personalmedicine.ModifiedAt = DateTime.Now;
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
            var dtolist = mapper.Map<List<PersonalMedicineDTO>>(personalMedicines).Where(p => p.Isdeleted == false);
            return dtolist.ToList();
        }

        public async Task<PersonalMedicineDTO> GetPersonalmedicineByIdAsync(int id)
        {
            var personalMedicines = await PersonalmedicineRepository.GetByIdAsync(id);
            var dto = mapper.Map<PersonalMedicineDTO>(personalMedicines);
            return dto;
        }

        public Task<List<PersonalMedicineDTO>> GetPersonalmedicinesByMedicineNameAsync(string medicineName)
        {
            var personalMedicines = PersonalmedicineRepository.GetAllAsync().Result;
            var mappedMedicines = mapper.Map<List<PersonalMedicineDTO>>(personalMedicines);

            return Task.FromResult(mappedMedicines.Where(md => md.Medicinename.Contains(medicineName, StringComparison.OrdinalIgnoreCase)).ToList());
        }

        public Task<List<PersonalMedicineDTO>> GetPersonalmedicinesByParentIdAsync(int parentId)
        {
            var personalMedicines = PersonalmedicineRepository.GetAllAsync().Result;
            var mappedMedicines = mapper.Map<List<PersonalMedicineDTO>>(personalMedicines);

            return Task.FromResult(mappedMedicines.Where(md => md.Parentid == parentId).ToList());
        }

        public async Task<List<Personalmedicine>> SearchPersonalmedicinesAsync(string searchTerm)
        {
            var Personalmedicines = await PersonalmedicineRepository.GetAllAsync();
            var filtered = Personalmedicines.Where(donation =>
                    donation.Medicinename.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    (donation.Parent != null && donation.Parent.Fullname.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))).ToList();
            return filtered;
        }

        public async void UpdatePersonalmedicine(UpdatePersonalMedicineDTO Personalmedicine, int id)
        {
            var PersonalmedicineEntity = PersonalmedicineRepository.GetByIdAsync(id).Result;
            if (PersonalmedicineEntity == null)
            {
                throw new KeyNotFoundException("Medicine donation not found.");
            }
            else
            {
                PersonalmedicineEntity.Medicinename = Personalmedicine.Medicinename;
                PersonalmedicineEntity.Studentid = Personalmedicine.Studentid;
                PersonalmedicineEntity.Parentid = Personalmedicine.Parentid;
                PersonalmedicineEntity.Quantity = Personalmedicine.Quantity;
                PersonalmedicineEntity.Receivedate = Personalmedicine.Receivedate;
                PersonalmedicineEntity.Expirydate = Personalmedicine.Expirydate;
                PersonalmedicineEntity.DeliveryStatus = Personalmedicine.DeliveryStatus;

                PersonalmedicineEntity.Note = Personalmedicine.Note;
                PersonalmedicineEntity.ModifiedAt = DateTime.Now;
                PersonalmedicineRepository.Update(PersonalmedicineEntity);
                PersonalmedicineRepository.Save();

            }


        }
        public Task<List<PersonalMedicineDTO>> GetPersonalmedicinesByApprovalAsync()
        {
            var Personalmedicines = PersonalmedicineRepository.GetAllAsync();
            var dto = mapper.Map<List<PersonalMedicineDTO>>(Personalmedicines.Result.Where(md => md.DeliveryStatus == PersonalMedicineStatus.Accepted));
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
                    List<ScheduleDetail> scheduledetails = new List<ScheduleDetail>();
                    Classroom classRoom = classRoomRepository.GetAllAsync().Result.FirstOrDefault(c => c.Classid == personalMedicine.Student.Classid);
                    var studentname = personalMedicine.Student.Fullname;
                    var MedicineScheduleList = medicineScheduleRepository.GetAllAsync().Result.Where(ms => ms.PersonalMedicineId == personalMedicine.Personalmedicineid).ToList();
                    foreach (var schedule in MedicineScheduleList)
                    {
                        scheduledetails.Add(ScheduleDetailList.FirstOrDefault(sd => sd.ScheduleDetailId == schedule.ScheduleDetailId));
                    }
                    var scheduling = mapper.Map<List<ScheduleDetailDTO>>(scheduledetails);
                    var request = new PersonalMedicineRequestDTO
                    {
                        Studentid = personalMedicine.Studentid,
                        StudentName = studentname,
                        ParentId = personalMedicine.Parentid,
                        ParentName = personalMedicine.Parent.Fullname,
                        ClassName = classRoom.Classname,
                        MedicineName = personalMedicine.Medicinename,
                        Quantity = personalMedicine.Quantity,
                        ExpiryDate = DateOnly.FromDateTime(personalMedicine.Expirydate),
                        Note = personalMedicine.Note ?? string.Empty,
                        PhoneNumber = personalMedicine.Parent.Phone,
                        PreferedTime = scheduling,
                        CreatedAt = personalMedicine.CreatedAt.HasValue
                            ? personalMedicine.CreatedAt.Value
                            : throw new InvalidOperationException("CreatedAt cannot be null.")
                    };
                    personalMedicineRequests.Add(request);
                }
            }
            var task = Task.FromResult(personalMedicineRequests);
            return await task;

        }
        public Task ApprovePersonalMedicine(ApprovalPersonalMedicineDTO dto, int id)
        {
            var personalMedicine = PersonalmedicineRepository.GetByIdAsync(id).Result;
            if (personalMedicine != null)
            {
                personalMedicine.DeliveryStatus = PersonalMedicineStatus.Accepted;
                personalMedicine.ModifiedByUserId = dto.ApprovedBy;
                personalMedicine.ModifiedAt = DateTime.Now;
                PersonalmedicineRepository.Update(personalMedicine);
                PersonalmedicineRepository.Save();
            }
            return Task.CompletedTask;
        }
        public Task RejectPersonalMedicine(ApprovalPersonalMedicineDTO dto, int id)
        {
            var personalMedicine = PersonalmedicineRepository.GetByIdAsync(id).Result;
            if (personalMedicine != null)
            {
                personalMedicine.DeliveryStatus = PersonalMedicineStatus.Rejected;
                personalMedicine.ModifiedByUserId = dto.ApprovedBy;
                personalMedicine.ModifiedAt = DateTime.Now;
                PersonalmedicineRepository.Update(personalMedicine);
                PersonalmedicineRepository.Save();

            }
            return Task.CompletedTask;
        }

    }
}
