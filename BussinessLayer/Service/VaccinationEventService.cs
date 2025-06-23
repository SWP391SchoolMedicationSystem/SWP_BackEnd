using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class VaccinationEventService : IVaccinationEventService
    {
        private readonly IVaccinationEventRepository _vaccinationEventRepo;
        private readonly IMapper _mapper;
        public VaccinationEventService(IVaccinationEventRepository vaccinationEventRepo, IMapper mapper)
        {
            _vaccinationEventRepo = vaccinationEventRepo;
            _mapper = mapper;
        }

        public async Task AddVaccinationEventAsync(VaccinationEventDTO eventDto)
        {
            await _vaccinationEventRepo.AddAsync(_mapper.Map<Vaccinationevent>(eventDto));
        }

        public async void DeleteVaccinationEvent(int id)
        {
            Vaccinationevent vaccinationevent = await _vaccinationEventRepo.GetByIdAsync(id);
            if(vaccinationevent != null)
            {
                vaccinationevent.Isdeleted = true;
                _vaccinationEventRepo.Update(vaccinationevent);
                _vaccinationEventRepo.Save();
            }
            else
            {
                throw new KeyNotFoundException($"Vaccination event with ID {id} not found.");
            }
        }

        public async Task<List<Vaccinationevent>> GetAllVaccinationEvents()
        {
            return await _vaccinationEventRepo.GetAllAsync();
        }

        public Task<Vaccinationevent> GetVaccinationEventById(int id)
        {
           return _vaccinationEventRepo.GetByIdAsync(id);
        }

        public void UpdateVaccinationEvent(VaccinationEventDTO eventDto)
        {
            _vaccinationEventRepo.Update(_mapper.Map<Vaccinationevent>(eventDto));
        }
    }
}
