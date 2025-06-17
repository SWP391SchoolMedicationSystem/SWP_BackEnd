using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace DataAccessLayer.Repository
{
    public class BlogRepo : GenericRepository<Blog>, IBlogRepo
    {
        public BlogRepo(SchoolMedicalSystemContext context) : base(context)
        {
        }
    }
}
