using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IHealthStatusServices
    {
        List<Healthstatus> GetHealthstatuses();
        Healthstatus GetHealthstatusByID(int id);
        void AddHealthStatus(Healthstatus healthstatus);
        void UpdateHealthstatus(Healthstatus healthstatus);
        void DeleteHealthstatus(int id);
        List<Healthstatus> GetHealthstatusesByCategoryId(int categoryId);
        List<Healthstatus> GetHealthstatusesByStudentId(int studentId);

    }
}
