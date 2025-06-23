using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Utils.Configurations;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;

namespace BussinessLayer.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IStaffService _staffService;
        private readonly IParentService _parentService;
        private readonly AppSetting _appSettings;
        public UserService(IUserRepository userRepository, IMapper mapper,
            IParentService parentService, IStaffService staffService, IOptionsMonitor<AppSetting> option)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _parentService = parentService;
            _staffService = staffService;
            _appSettings = option.CurrentValue;
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
            try
            {
                List<User> users = await _userRepository.GetAllAsync();
                User user = users.FirstOrDefault(u => u.Email == dto.Email);
                if (user.IsStaff)
                {
                    return await _staffService.GenerateToken(dto);
                }
                else return await _parentService.GenerateToken(dto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new InvalidOperationException("Login failed. Please check your credentials.");
            }
        }
        public void Save()
        {
            _userRepository.Save();
        }

        public void Update(User entity)
        {
            _userRepository.Update(entity);
        }

        public async Task<string> ValidateGoogleToken(string token)
        {

                try
                {
                    var payload = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings
                    {
                        Audience = new[] { _appSettings.GoogleClientId }
                    });
                    string email = payload.Email;
                    var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == email);
                if (user == null) return null;
                    if (user.IsStaff == true)
                    {
                        LoginDTO stafflogin = _mapper.Map<LoginDTO>(user);
                        return await _staffService.GenerateToken(stafflogin);
                    }
                    else
                    {
                        LoginDTO parentLogin = _mapper.Map<LoginDTO>(user);
                        return await _parentService.GenerateToken(parentLogin);
                    }
                }
                catch (Exception e) {
                return null;
            }
            }
    
    }
}

