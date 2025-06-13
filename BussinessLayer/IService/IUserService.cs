using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.IService
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User entity);
        void Update(User entity);
        void Delete(int ID);
        void Save();
    }
}
