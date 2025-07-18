using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.Constants;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;

namespace BussinessLayer.Service
{
    public class ScheduleDetailService(IMapper mapper, IScheduleDetailRepo scheduleDetailRepo, IEmailService emailService) : IScheduleDetailService
    {
        public async Task AddScheduleDetailAsync(ScheduleDetailDTO scheduleDetail)
        {
            scheduleDetailRepo.AddAsync(mapper.Map<Scheduledetail>(scheduleDetail));
            scheduleDetailRepo.Save();
        }

        public Task DeleteScheduleDetailAsync(int id)
        {
            var scheduleDetail = scheduleDetailRepo.GetByIdAsync(id);
            if (scheduleDetail != null)
            {
                scheduleDetailRepo.Delete(scheduleDetail.Result.Scheduledetailid);
                scheduleDetailRepo.Save();
            }
            return Task.CompletedTask;
        }

        public Task<List<Scheduledetail>> GetAllScheduleDetailsAsync()
        {
            return scheduleDetailRepo.GetAllAsync();
        }

        public Task<Scheduledetail> GetScheduleDetailByIdAsync(int id)
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

                    entity.Starttime = scheduleDetail.Starttime;
                    entity.Endtime = scheduleDetail.Endtime;
                    entity.Dayinweek = scheduleDetail.Dayinweek;
                    scheduleDetailRepo.Update(entity);
                    scheduleDetailRepo.Save();
                }
            });
        }
    }
}
