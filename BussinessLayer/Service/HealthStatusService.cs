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
    public class HealthStatusService : IHealthStatusServices
    {
        private readonly IHealthStatusRepository _healthStatusRepository;
        private readonly IStudentRepo _studentrepo;
        private readonly IHealthStatusCategoryRepository _healthStatusCategoryRepository;
        private readonly IMapper _mapper;
        public HealthStatusService(IHealthStatusRepository healthStatusRepository, IStudentRepo studentrepo, IHealthStatusCategoryRepository healthStatusCategoryRepository, IMapper mapper)
        {
            _healthStatusRepository = healthStatusRepository;
            _studentrepo = studentrepo;
            _healthStatusCategoryRepository = healthStatusCategoryRepository;
            _mapper = mapper;
        }

        public async Task AddHealthStatus(Healthstatus healthstatus)
        {
            healthstatus.CreatedDate = DateTime.Now;
            await _healthStatusRepository.AddAsync(healthstatus);
        }

        public async void DeleteHealthstatus(int id)
        {
            var healthstatus = await _healthStatusRepository.GetByIdAsync(id);
            if(healthstatus != null)
            {
                healthstatus.IsDeleted = true;
                _healthStatusRepository.Update(healthstatus);
                _healthStatusRepository.Save();
            }
            else
            {
                throw new KeyNotFoundException($"Health status with ID {id} not found.");
            }

        public Healthstatus GetHealthstatusByID(int id)
        {
            return _healthStatusRepository.GetByIdAsync(id).Result ?? throw new KeyNotFoundException($"Health status with ID {id} not found.");
        }

        public List<Healthstatus> GetHealthstatuses()
        {
            return _healthStatusRepository.GetAll().Where(h => !h.IsDeleted).ToList();
        }

        public List<Healthstatus> GetHealthstatusesByCategoryId(int categoryId)
        {
            return _healthStatusRepository.GetAll()
                .Where(h => h.HealthStatusCategory == categoryId && !h.IsDeleted)
                .ToList();
        }

        public List<Healthstatus> GetHealthstatusesByStudentId(int studentId)
        {
            return _healthStatusRepository.GetAll()
                .Where(h => h.StudentId == studentId && !h.IsDeleted)
                .ToList();
        }

        public void UpdateHealthstatus(Healthstatus healthstatus)
        {
            _healthStatusRepository.Update(healthstatus);
        }
    }
}
