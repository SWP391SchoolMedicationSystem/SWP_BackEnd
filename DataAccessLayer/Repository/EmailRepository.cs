using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class EmailRepository : GenericRepository<EmailTemplate>, IEmailRepo
    {
        DbSet<EmailTemplate> _dbset;
        public EmailRepository(SchoolMedicalSystemContext context) : base(context)
        {
            _dbset = context.Set<EmailTemplate>();
        }

        public async Task<EmailTemplate?> GetEmailTemplateByIdAsync(int id)
        {
            return await _dbset.FirstOrDefaultAsync(e => e.EmailTemplateId == id && !e.IsDeleted);
        }


    }
}
