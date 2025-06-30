using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Students;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class VaccinationRecordService : IVaccinationRecordService
    {
        private readonly IVaccinationRecordRepository _vaccinationRecordRepo;
        private readonly IStudentRepo _studentRepo;
        private readonly IVaccinationEventRepository _vaccinationEventRepository;
        private readonly IMapper _mapper;
        public VaccinationRecordService(IVaccinationRecordRepository vaccinationRecordRepo,
            IVaccinationEventRepository vaccinationEventRepository,IStudentRepo studentRepo,
            IMapper mapper)
        {
            _vaccinationRecordRepo = vaccinationRecordRepo;
            _mapper = mapper;
            _vaccinationEventRepository = vaccinationEventRepository;
            _studentRepo = studentRepo;
        }

        public async Task AddVaccinationRecordAsync(VaccinationRecordDTO record)
        {
           await _vaccinationRecordRepo.AddAsync(_mapper.Map<Vaccinationrecord>(record));
        }

        public async void DeleteVaccinationRecord(int id)
        {
            Vaccinationrecord vaccinationrecord = await _vaccinationRecordRepo.GetByIdAsync(id);
            if (vaccinationrecord != null)
            {
                vaccinationrecord.Isdeleted = true;
                _vaccinationRecordRepo.Update(vaccinationrecord);
                _vaccinationRecordRepo.Save();
            }
            else
            {
                throw new Exception("Vaccination record not found");
            }
        }

        public async Task<List<VaccinationRecordStudentEvent>> GetAllVaccinationRecords()
        {
            var list = await _vaccinationRecordRepo.GetAllAsync();
            List<VaccinationRecordStudentEvent> recordlist = _mapper.Map<List<VaccinationRecordStudentEvent>>(list);
            foreach (var record in recordlist)
            {
                record.Students.Add(_mapper.Map<StudentDTO>(await _studentRepo.GetByIdAsync(record.Studentid)));
                record.Vaccinationevents.Add(_mapper.Map<VaccinationEventDTO>(await _vaccinationEventRepository.GetByIdAsync(record.Vaccinationeventid)));
            }
            return recordlist;
        }

        public async Task<Vaccinationrecord> GetVaccinationRecordById(int id)
        {
            return await _vaccinationRecordRepo.GetByIdAsync(id) ?? throw new Exception("Vaccination record not found");
        }

        public void UpdateVaccinationRecord(VaccinationRecordDTO record)
        {
             _vaccinationRecordRepo.Update(_mapper.Map<Vaccinationrecord>(record));
            _vaccinationRecordRepo.Save();

        }
    }
}
