using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Parents;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IParentService
    {
        Task<List<ParentDTO>> GetAllParentsAsync();
        Task<ParentDTO> GetParentByIdAsync(int id);
        Task<ParentDTO> GetParentByEmailAsync(string email);
        Task<ParentVaccineEvent> GetParentByEmailForEvent(string email);
        Task<int> AddParentAsync(ParentRegister parent);
        Task UpdateParent(ParentUpdate parent);
        Task DeleteParent(int id);
        Task<String> GenerateToken(LoginDTO login);
        Task<String> GenerateGoogleToken(LoginGoogleDTO login);
        Task<string> ValidateGoogleToken(string token);
    }
}
