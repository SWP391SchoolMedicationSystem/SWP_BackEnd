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
    public class ConsultationRequestService(IConsulationRepository consulationRepository, IConsultationTypeRepo consultationTypeRepo,
        IParentRepository parentRepository,
        IStaffRepository staffRepository,
        IStudentRepo studentRepo,
        IUserRepository _userRepository,
        IMapper mapper) : IConsultationService
    {
        public async Task<Consultationrequest> AddConsultationRequest(CreateConsultationDTO request)
        {

            var type = consultationTypeRepo.GetByIdAsync(request.Requesttypeid);
            var consultationRequest = mapper.Map<Consultationrequest>(request);
            consultationRequest.Requesttype = type.Result;
            consultationRequest.Createddate = DateTime.Now;
            consultationRequest.Requestdate = DateTime.Now;
            consultationRequest.Status = "Pending";
            await consulationRepository.AddAsync(consultationRequest);
            consulationRepository.Save();
            return Task.FromResult(consultationRequest).Result;
        }

        public async Task ApproveRequest(ApprovalConsultationDTO dto)
        {
            var request = await consulationRepository.GetByIdAsync(dto.Consultationid);
            request.Status = "Approve";
            request.Staffid = dto.Staffid;
            request.Modifiedby = dto.Modifiedby;
            request.Modifieddate = DateTime.Now;
            consulationRepository.Update(request);
            consulationRepository.Save();
        }
        public async Task RejectRequest(ApprovalConsultationDTO dto)
        {
            var request = await consulationRepository.GetByIdAsync(dto.Consultationid);
            request.Status = "Reject";
            request.Staffid = dto.Staffid;
            request.Modifiedby = dto.Modifiedby;
            request.Modifieddate = DateTime.Now;
            consulationRepository.Update(request);
            consulationRepository.Save();
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
        public Task<List<ConsultationDTO>> GetAllConsultationRequestsAsync()
        {
            var consultation = consulationRepository.GetAllAsync().Result;
            var dto = mapper.Map<List<ConsultationDTO>>(consultation);
            foreach (var item in dto)
            {
                // Set the image URL to be absolute
                if (item.Createdby != null && item.Createdby != 0)
                {
                    if (_userRepository.GetByIdAsync(item.Createdby).Result.IsStaff)
                        item.CreateByName = staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == item.Createdby)?.Fullname ?? "Unknown";
                    else
                        item.CreateByName = parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == item.Createdby)?.Fullname ?? "Unknown";
                }
                else item.CreateByName = "Unknown";

                item.Staffname = (item.Staffid != null && item.Staffid != 0)
                    ? staffRepository.GetByIdAsync(item.Staffid.Value).Result?.Fullname
                    : "Unassigned";
                item.ParentName = (item.Parentid != null && item.Parentid != 0) ? parentRepository.GetByIdAsync(item.Parentid).Result?.Fullname : "Không có Phụ Huynh";
                item.StudentName = (item.Studentid != null && item.Studentid != 0) ? studentRepo.GetByIdAsync(item.Studentid).Result?.Fullname : "Không có Học Sinh";
            }
            return Task.FromResult(dto);
        }
        public Task<ConsultationDTO> GetConsultationRequestByIdAsync(int id)
        {
            var consultation = consulationRepository.GetByIdAsync(id);
            var item = mapper.Map<ConsultationDTO>(consultation);
            if (item.Createdby != null && item.Createdby != 0)
            {
                if (_userRepository.GetByIdAsync(item.Createdby).Result.IsStaff)
                    item.CreateByName = staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == item.Createdby)?.Fullname ?? "Unknown";
                else
                    item.CreateByName = parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == item.Createdby)?.Fullname ?? "Unknown";
            }
            else item.CreateByName = "Unknown";

            item.Staffname = (item.Staffid != null && item.Staffid != 0)
                ? staffRepository.GetByIdAsync(item.Staffid.Value).Result?.Fullname
                : "Unassigned";
            item.ParentName = (item.Parentid != null && item.Parentid != 0) ? parentRepository.GetByIdAsync(item.Parentid).Result?.Fullname : "Không có Phụ Huynh";
            item.StudentName = (item.Studentid != null && item.Studentid != 0) ? studentRepo.GetByIdAsync(item.Studentid).Result?.Fullname : "Không có Học Sinh";
            return Task.FromResult(item);

        }

        public Task<List<ConsultationDTO>> GetPendingRequest()
        {
            var consultation = consulationRepository.GetAllAsync();
            var dto = mapper.Map<List<ConsultationDTO>>(consultation);
            foreach (var item in dto)
            {
                if (item.Createdby != null && item.Createdby != 0)
                {
                    if (_userRepository.GetByIdAsync(item.Createdby).Result.IsStaff)
                        item.CreateByName = staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == item.Createdby)?.Fullname ?? "Unknown";
                    else
                        item.CreateByName = parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == item.Createdby)?.Fullname ?? "Unknown";
                }
                else item.CreateByName = "Unknown";

                item.Staffname = (item.Staffid != null && item.Staffid != 0)
                    ? staffRepository.GetByIdAsync(item.Staffid.Value).Result?.Fullname
                    : "Unassigned";
                item.ParentName = (item.Parentid != null && item.Parentid != 0) ? parentRepository.GetByIdAsync(item.Parentid).Result?.Fullname : "Không có Phụ Huynh";
                item.StudentName = (item.Studentid != null && item.Studentid != 0) ? studentRepo.GetByIdAsync(item.Studentid).Result?.Fullname : "Không có Học Sinh";
            }
            dto = dto.Where(dto => dto.Status == "Pending").ToList();
            return Task.FromResult(dto);

        }


        public Task<Consultationrequest> UpdateConsulationRequest(UpdateConsultationDTO consultationRequest)
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
