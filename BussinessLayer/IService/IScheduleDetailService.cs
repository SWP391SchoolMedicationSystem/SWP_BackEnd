using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IScheduleDetailService 
    {
        public Task<List<ScheduleDetail>> GetAllScheduleDetailsAsync();
        public Task<ScheduleDetail> GetScheduleDetailByIdAsync(int id);
        public Task AddScheduleDetailAsync(ScheduleDetailDTO scheduleDetail);
        public Task UpdateScheduleDetailAsync(ScheduleDetailDTO scheduleDetail,int id);
        public Task DeleteScheduleDetailAsync(int id);
    }
}
