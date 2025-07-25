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
    public class MedicineCategoryRepository : GenericRepository<Medicinecategory>, IMedicineCategoryRepository
    {
        private readonly DbSet<Medicinecategory> _dbSet;
        public MedicineCategoryRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbSet = context.Set<Medicinecategory>();
        }
    }
}
