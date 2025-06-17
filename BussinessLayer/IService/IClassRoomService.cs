using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IClassRoomService
    {
        Task<List<Classroom>> GetAllClassRoomsAsync();
        Task<Classroom> GetClassRoomByName(string name);

        void DeleteClassRoom(int id);
    }
}
