﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.IRepository
{
    public interface IEmailRepo : IGenericRepository<EmailTemplate>
    {
        Task<EmailTemplate?> GetEmailTemplateByIdAsync(int id);
    }
}
