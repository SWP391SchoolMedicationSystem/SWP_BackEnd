using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IStaffService _staffService;
        private readonly IParentService _parentService;
        public UserService(IUserRepository userRepository, IMapper mapper,
            IParentService parentService, IStaffService staffService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _parentService = parentService;
            _staffService = staffService;
        }
        public async Task AddAsync(User entity)
        {
            await _userRepository.AddAsync(entity);
        }

        public void Delete(int ID)
        {
            if (GetByIdAsync(ID) != null)
            {
                _userRepository.Delete(ID);
            }
            else
            {
                throw new InvalidOperationException("User not found.");
            }
        }


        public Task<List<User>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public async Task<string> Login(LoginDTO dto)
        {
            List<User> users = await _userRepository.GetAllAsync();
            User user = users.FirstOrDefault(u => u.Email == dto.Email);
            if (user.IsStaff) {
                return await _staffService.GenerateToken(dto);
            }
            else return await _parentService.GenerateToken(dto);
        }
        public void Save()
        {
            _userRepository.Save();
        }

        public void Update(User entity)
        {
            _userRepository.Update(entity);
        }
    }
}

