﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.IRepository
{
    public interface IHealthCheckRepo : IGenericRepository<Healthcheck>
    {
        Task<List<Healthcheck>> GetHealthChecksByStudentIdAsync(int studentId);
        Task<Healthcheck?> GetHealthCheckByIdAsync(int checkId);

    }
}
