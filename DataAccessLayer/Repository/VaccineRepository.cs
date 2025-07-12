using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class VaccineRepository : GenericRepository<Vaccine>, IVaccineRepository
    {
        private readonly DbSet<Vaccine> vaccines;
        private readonly IMapper _mapper;
        public VaccineRepository(SchoolMedicalSystemContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            vaccines = context.Set<Vaccine>();
        }
        public async Task<List<Vaccine>> GetAllVaccinesAsync()
        {
            return await vaccines
                .Include(b => b.ModifiedByUser).ThenInclude(b => b.StaffUsers)
                .Include(b => b.CreatedByUser).ThenInclude(b => b.StaffUsers)

                .ToListAsync();
        }
    }
}
