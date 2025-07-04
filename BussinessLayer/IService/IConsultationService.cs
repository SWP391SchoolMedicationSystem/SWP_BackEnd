using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.Consultations;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IConsultationService
    {
        Task<Consultationrequest> AddConsultationRequest(CreateConsultationDTO request);
        Task<Consultationrequest> UpdateConsulationRequest(UpdateConsultationDTO consultationRequest);
        void DeleteConsultationRequest(int id);
        Task<List<ConsultationDTO>> GetAllConsultationRequestsAsync();
        Task<ConsultationDTO> GetConsultationRequestByIdAsync(int id);
        Task<List<ConsultationDTO>> GetPendingRequest();
        Task ApproveRequest(ApprovalConsultationDTO dto);
        Task RejectRequest(ApprovalConsultationDTO dto);


    }
}
