using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IParentRepository _parentRepository;
        private readonly IStudentRepo _studentRepository;

        public DashboardService(IUserRepository userRepository, IStaffRepository staffRepository, IParentRepository parentRepository, IStudentRepo studentRepository)
        {
            _userRepository = userRepository;
            _staffRepository = staffRepository;
            _parentRepository = parentRepository;
            _studentRepository = studentRepository;
        }

        public async Task<DashboardDTO> UsersStastic()
        {
            var totalUsers = await _userRepository.GetAllAsync();
            var totalStaff = await _staffRepository.GetAllAsync();
            var totalParents = await _parentRepository.GetAllAsync();
            var totalStudents = await _studentRepository.GetAllAsync();
            var activeUsers = totalUsers.Count(u => !u.IsDeleted);
            var totalManagers = totalStaff.Count(s => s.Roleid == 2);
            var totalNurses = totalStaff.Count(s => s.Roleid == 3);
            return new DashboardDTO
            {
                totalUsers = totalUsers.Count,
                totalStaff = totalStaff.Count,
                totalParents = totalParents.Count,
                totalStudents = totalStudents.Count,
                activeUsers = activeUsers,
                totalManagers = totalManagers,
                totalNurses = totalNurses
            };
        }
    }
}
