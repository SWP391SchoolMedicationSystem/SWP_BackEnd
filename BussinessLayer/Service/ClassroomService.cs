using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class ClassroomService : IClassRoomService
    {
        private readonly IClassRoomRepository _classroomRepository;
        public ClassroomService(IClassRoomRepository classroomRepository)
        {
            _classroomRepository = classroomRepository;

        }

        public async Task DeleteClassRoom(int id)
        {
            var classroom = await _classroomRepository.GetByIdAsync(id);
            if (classroom != null)
            {
                classroom.IsDeleted = true;
                _classroomRepository.Update(classroom);
                _classroomRepository.Save();

            }
        }

        public async Task<List<Classroom>> GetAllClassRoomsAsync()
        {
            return await _classroomRepository.GetAllAsync();
        }

        public async Task<Classroom> GetClassRoomByName(string name)
        {
            var classlist = await _classroomRepository.GetAllAsync();
            Classroom? classes = classlist.FirstOrDefault(c => c.Classname.Equals(name, StringComparison.OrdinalIgnoreCase));
            return classes ?? throw new InvalidOperationException($"Classroom with name '{name}' not found.");
        }
    }
}
