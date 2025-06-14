using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Utils.Configurations;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BussinessLayer.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepo _notificationdRepository;
        private readonly INotificationParentDetailRepo _notificationParentDetailRepo;
        private readonly IMapper _mapper;
        private readonly AppSetting _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IParentRepository _parentRepository;
        private readonly IStaffRepository _staffRepository;
        public NotificationService(
            INotificationRepo notificationdRepository,
            INotificationParentDetailRepo notificationParentDetailRepo,
            IParentRepository parentRepository,
            IMapper mapper,
            IOptionsMonitor<AppSetting> option,
            IHttpContextAccessor httpContextAccessor,
            IStaffRepository staffRepository)
        {
            _notificationdRepository = notificationdRepository;
            _notificationParentDetailRepo = notificationParentDetailRepo;
            _parentRepository = parentRepository;
            _mapper = mapper;
            _appSettings = option.CurrentValue;
            _httpContextAccessor = httpContextAccessor;
            _staffRepository = staffRepository;
        }
        public void CreateNotification(NotificationDTO dto)
        {
            var entity = _mapper.Map<Notification>(dto);
            entity.CreatedAt = DateTime.Now;
            entity.Createddate = DateTime.Now;
            entity.IsDeleted = false;

            _notificationdRepository.AddAsync(entity);
            _notificationdRepository.Save();
        }

        public void CreateNotificationForParent(NotificationDTO dto)
        {
            // Create and save the Notification  
            var notification = _mapper.Map<Notification>(dto);
            notification.CreatedAt = DateTime.Now;
            notification.Createddate = DateTime.Now;
            notification.IsDeleted = false;

            _notificationdRepository.AddAsync(notification).GetAwaiter().GetResult();
            _notificationdRepository.Save();

            // Get all active parents (not deleted)  
            var allParents = _parentRepository.GetAllAsync().GetAwaiter().GetResult()
                                .Where(p => !p.IsDeleted).ToList();

            // Create a NotificationParentDetail for each parent  
            foreach (var parent in allParents)
            {
                var detail = new NotificationParentDetail
                {
                    NotificationId = notification.NotificationId, // Fix: Use notification.NotificationId directly  
                    ParentId = parent.Parentid,
                    Message = dto.Title, // or customize per use case  
                    IsRead = false,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    CreatedBy = notification.Createdby // optionally get from context  
                };

                _notificationParentDetailRepo.AddAsync(detail);
            }

            _notificationParentDetailRepo.Save();
        }

        public void CreateNotificationForStaff(NotificationDTO dto)
        {
            // Create and save the Notification  
            var notification = _mapper.Map<Notification>(dto);
            notification.CreatedAt = DateTime.Now;
            notification.Createddate = DateTime.Now;
            notification.IsDeleted = false;

            _notificationdRepository.AddAsync(notification).GetAwaiter().GetResult();
            _notificationdRepository.Save();

            // Get all active staff (not deleted)  
            var allStaff = _staffRepository.GetAllAsync().GetAwaiter().GetResult()
                                .Where(s => !s.IsDeleted).ToList();
            // Create a NotificationParentDetail for each staff  
            foreach (var staff in allStaff)
            {
                var detail = new NotificationParentDetail
                {
                    NotificationId = notification.NotificationId, // Fix: Use notification.NotificationId directly  
                    ParentId = staff.Staffid,
                    Message = dto.Title, // or customize per use case  
                    IsRead = false,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    CreatedBy = notification.Createdby // optionally get from context  
                };
                _notificationParentDetailRepo.AddAsync(detail);
            }
            _notificationParentDetailRepo.Save();
        }

        public void DeleteNotification(int id)
        {
            var entity = _notificationdRepository.GetByIdAsync(id).Result;
            if (entity != null)
            {
                entity.IsDeleted = true; // Soft delete
//                _notificationdRepository.Delete(id);
                _notificationdRepository.Save();
            }
        }

        public List<Notification> GetAllNotifications()
        {
            var notifications = _notificationdRepository.GetAllAsync().Result;
            return notifications.Where(n => !n.IsDeleted).ToList();
        }
    }
}
