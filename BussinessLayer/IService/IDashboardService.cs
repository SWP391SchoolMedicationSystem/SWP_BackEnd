﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;

namespace BussinessLayer.IService
{
    public interface IDashboardService
    {
        Task<DashboardDTO> UsersStastic();
    }
}
