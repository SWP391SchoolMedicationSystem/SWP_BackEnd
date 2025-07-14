using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO.Consultations;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;

namespace BussinessLayer.Service
{
    public class ConsultationTypeService(IConsultationTypeRepo _consultationtype, IMapper mapper) : IConsultationTypeService
    {

        public async Task<Consultationtype> AddConsultationTypeAsync(CreateConsultationTypeDTO consultationType)
        {
            var entity = mapper.Map<Consultationtype>(consultationType);
            await _consultationtype.AddAsync(entity);
            _consultationtype.Save();
            return entity;

        }

        public void DeleteConsultationType(int id)
        {
            var entity = _consultationtype.GetByIdAsync(id).Result;
            if (entity != null)
            {
                entity.Isdeleted = true;
                _consultationtype.Update(entity);
                _consultationtype.Save();
            }
        }

        public Task<List<Consultationtype>> GetAllConsultationTypesAsync()
        {
            return _consultationtype.GetAllAsync();
        }

        public Task<Consultationtype> GetConsultationTypeByIdAsync(int id)
        {
            return _consultationtype.GetByIdAsync(id);
        }

        public Task<Consultationtype> UpdateConsultationTypeAsync(Consultationtype consultationType)
        {
            var existingEntity = _consultationtype.GetByIdAsync(consultationType.Typeid).Result;
            if (existingEntity != null)
            {
                existingEntity.Typename = consultationType.Typename;
                existingEntity.Description = consultationType.Description;
                _consultationtype.Update(existingEntity);
                _consultationtype.Save();
                return Task.FromResult(existingEntity);
            }
            return null;
        }
    }
}
