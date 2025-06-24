using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.Consultation;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IConsultationService
    {
        Task<Consultationrequest> AddConsultationRequest(ConsultationRequestDTO request);
        Task<Consultationrequest> UpdateConsulationRequest(Consultationrequest consultationRequest);
        void DeleteConsultationRequest(int id);
        Task<List<Consultationrequest>> GetAllConsultationRequestsAsync();
        Task<Consultationrequest> GetConsultationRequestByIdAsync(int id);
    }
}
