using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.IService
{
    public interface IFormService
    {
        Task<List<FormDTO>> GetAllFormsAsync();
        Task<FormDTO> GetFormByIdAsync(int id);
        Task<FormDTO> CreateFormAsync(CreateFormDTO formDto, string createdBy);
        Task<FormDTO> UpdateFormAsync(UpdateFormDTO formDto, string modifiedBy);
        Task<bool> DeleteFormAsync(int id, string deleteBy);
        Task<List<FormDTO>> GetFormsByParentIdAsync(int parentId);
        Task<List<FormDTO>> GetFormsByCategoryIdAsync(int categoryId);
        Task<bool> AcceptFormAsync(ResponseFormDTO dto, string acceptBy);
        Task<bool> DeclineFormAsync(ResponseFormDTO dto, string declineBy);
    }
}
