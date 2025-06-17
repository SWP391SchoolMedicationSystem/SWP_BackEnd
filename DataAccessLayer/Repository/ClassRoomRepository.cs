﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class ClassRoomRepository : GenericRepository<Classroom>, IClassRoomRepository
    {
        private readonly DbSet<Classroom> _dbset;
        public ClassRoomRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<Classroom>();
        }
    }
}
