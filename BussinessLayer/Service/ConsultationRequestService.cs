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
using DataAccessLayer.Repository;

namespace BussinessLayer.Service
{
    public class ConsultationRequestService(IConsulationRepository consulationRepository, IConsultationTypeRepo consultationTypeRepo,
        IParentRepository parentRepository, IStaffRepository staffRepository, IStudentRepo studentRepo,
        IMapper mapper) : IConsultationService
    {
        public async Task<Consultationrequest> AddConsultationRequest(ConsultationRequestDTO request)
        {

            var type = consultationTypeRepo.GetByIdAsync(request.Requesttypeid);
            var consultationRequest = mapper.Map<Consultationrequest>(request);
            consultationRequest.Requesttype = type.Result;
            consultationRequest.Createddate = DateTime.Now;
            consultationRequest.Requestdate = DateTime.Now;
            await consulationRepository.AddAsync(consultationRequest);
            consulationRepository.Save();
            return Task.FromResult(consultationRequest).Result;
        }
        public void DeleteConsultationRequest(int id)
        {
            Task<Consultationrequest> entity = consulationRepository.GetByIdAsync(id);
            if (entity.Result != null)
            {
                entity.Result.Isdelete = true;
                consulationRepository.Update(entity.Result);
                consulationRepository.Save();
            }
        }
        public Task<List<Consultationrequest>> GetAllConsultationRequestsAsync()
        {
            var consultation = consulationRepository.GetAllAsync();
            return consultation;
        }
        public Task<Consultationrequest> GetConsultationRequestByIdAsync(int id)
        {
            return consulationRepository.GetByIdAsync(id);
        }
        public Task<Consultationrequest> UpdateConsulationRequest(Consultationrequest consultationRequest)
        {
            var existingRequest = consulationRepository.GetByIdAsync(consultationRequest.Consultationid);
            if (existingRequest != null)
            {
                existingRequest.Result.Parentid = consultationRequest.Parentid;
                existingRequest.Result.Studentid = consultationRequest.Studentid;
                existingRequest.Result.Requesttypeid = consultationRequest.Requesttypeid;
                existingRequest.Result.Requestdate = consultationRequest.Requestdate;
                existingRequest.Result.Scheduledate = consultationRequest.Scheduledate;
                existingRequest.Result.Title = consultationRequest.Title;
                existingRequest.Result.Description = consultationRequest.Description;
                existingRequest.Result.Status = consultationRequest.Status;
                existingRequest.Result.Staffid = consultationRequest.Staffid;
                existingRequest.Result.Isdelete = consultationRequest.Isdelete;
                existingRequest.Result.Modifiedby = consultationRequest.Modifiedby;
                existingRequest.Result.Modifieddate = DateTime.Now;
                consulationRepository.Update(existingRequest.Result);
                consulationRepository.Save();
                return Task.FromResult(existingRequest.Result);
            }
            return Task.FromResult(existingRequest.Result);
        }
    }
}
