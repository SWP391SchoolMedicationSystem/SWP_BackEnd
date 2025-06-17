using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Parents;

namespace BussinessLayer.IService
{
    public interface IParentService
    {
        Task<List<ParentDTO>> GetAllParentsAsync();
        Task<ParentDTO> GetParentByIdAsync(int id);
        Task AddParentAsync(ParentRegister parent);
        void UpdateParent(ParentUpdate parent);
        void DeleteParent(int id);
        Task<String> GenerateToken(LoginDTO login);
        Task<string> ValidateGoogleToken(string token);
    }
}
