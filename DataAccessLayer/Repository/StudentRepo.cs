using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.DTO.Parents;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class StudentRepo : GenericRepository<Student>, IStudentRepo
    {
        private readonly IMapper _mapper;
        private readonly DbSet<Student> _dbset;
        public StudentRepo(SchoolMedicalSystemContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _dbset = context.Set<Student>();
        }
        public List<Student> GetAllStudents()
        {
            return _dbset.Include(s => s.Parent)
                         .Include(s => s.Class)
                         .ToList();
        }
        public async Task<List<Student>> GetAllAsync()
        {
            return await _dbset.Include(s => s.Parent)
                               .Include(s => s.Class)
                               .ToListAsync();
        }

    }
}
