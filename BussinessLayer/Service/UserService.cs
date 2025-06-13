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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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

