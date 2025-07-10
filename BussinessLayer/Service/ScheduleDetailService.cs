using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class ScheduleDetailService(IMapper mapper, IScheduleDetailRepo scheduleDetailRepo) : IScheduleDetailService
    {
        public Task AddScheduleDetailAsync(ScheduleDetailDTO scheduleDetail)
        {
            scheduleDetailRepo.AddAsync(mapper.Map<ScheduleDetail>(scheduleDetail));
            scheduleDetailRepo.Save();
            return Task.CompletedTask;
        }

        public Task DeleteScheduleDetailAsync(int id)
        {
            var scheduleDetail = scheduleDetailRepo.GetByIdAsync(id);
            if (scheduleDetail != null)
            {
                scheduleDetailRepo.Delete(scheduleDetail.Result.ScheduleDetailId);
                scheduleDetailRepo.Save();
            }
            return Task.CompletedTask;
        }

        public Task<List<ScheduleDetail>> GetAllScheduleDetailsAsync()
        {
            return scheduleDetailRepo.GetAllAsync();
        }

        public Task<ScheduleDetail> GetScheduleDetailByIdAsync(int id)
        {
            return scheduleDetailRepo.GetByIdAsync(id);
        }

        public Task UpdateScheduleDetailAsync(ScheduleDetailDTO scheduleDetail, int id)
        {
            return Task.Run(() =>
            {
                var entity = scheduleDetailRepo.GetByIdAsync(id).Result;
                if (entity != null)
                {

                    entity.TimeSlot = scheduleDetail.Starttime;
                    entity.DayOfWeek = scheduleDetail.Dayinweek;
                    scheduleDetailRepo.Update(entity);
                    scheduleDetailRepo.Save();
                }
            });
        }
    }
}
