using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IHealthCheckService
    {
        List<Healthcheck> GetAllHealthCheck();

    }
}
