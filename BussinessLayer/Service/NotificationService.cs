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
    //comment
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

            _notificationdRepository.AddAsync(entity).GetAwaiter().GetResult(); // Fix: Use GetAwaiter().GetResult() to handle async void issue  
            _notificationdRepository.Save();
        }

        public void CreateNotificationForParent(NotificationDTO dto)
        {
            try
            {
                // Get all active parents FIRST - before any modifications
                var parentEntities = _parentRepository.GetAll();
                var activeParents = parentEntities.Where(p => !p.IsDeleted).ToList();

                // Create and save the Notification
                var notification = _mapper.Map<Notification>(dto);
                notification.CreatedAt = DateTime.Now;
                notification.Createddate = DateTime.Now;
                notification.IsDeleted = false;

                _notificationdRepository.Add(notification);
                _notificationdRepository.Save();

                // Create notification details for each parent
                foreach (var parent in activeParents)
                {
                    var detail = new NotificationParentDetail
                    {
                        NotificationId = notification.NotificationId,
                        ParentId = parent.Parentid,
                        Message = dto.Message,
                        IsRead = false,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                        CreatedBy = notification.Createdby,
                        ModifiedDate = DateTime.Now, // Use DateTime.Now instead of notification.Modifieddate
                        ModifiedBy = notification.Modifiedby
                    };
                    _notificationParentDetailRepo.Add(detail);
                }
                _notificationParentDetailRepo.Save();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create notification for parents: {ex.Message}", ex);
            }
        }

        public void CreateNotificationForStaff(NotificationDTO dto)
        {
            // Create and save the Notification  
            var notification = _mapper.Map<Notification>(dto);
            notification.CreatedAt = DateTime.Now;
            notification.Createddate = DateTime.Now;
            notification.IsDeleted = false;

            _notificationdRepository.AddAsync(notification).GetAwaiter().GetResult(); // Fix: Use GetAwaiter().GetResult() to handle async void issue  
            _notificationdRepository.Save();

            // Get all active staff (not deleted)  
            var allStaff = _staffRepository.GetAllAsync().GetAwaiter().GetResult()
                                .Where(s => !s.IsDeleted).ToList();
            // Create a NotificationParentDetail for each staff  
            foreach (var staff in allStaff)
            {
                var detail = new NotificationParentDetail
                {
                    NotificationId = notification.NotificationId,
                    ParentId = staff.Staffid,
                    Message = dto.Title,
                    IsRead = false,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    CreatedBy = notification.Createdby
                };
                _notificationParentDetailRepo.AddAsync(detail).GetAwaiter().GetResult(); // Fix: Use GetAwaiter().GetResult() to handle async void issue  
            }
            _notificationParentDetailRepo.Save();
        }

        public void DeleteNotification(int id)
        {
            var entity = _notificationdRepository.GetByIdAsync(id).Result;
            if (entity != null)
            {
//                entity.IsDeleted = true; // Soft delete
                _notificationdRepository.Delete(id);
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
