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
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BussinessLayer.Service
{
    //comment
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepo _notificationdRepository;
        private readonly INotificationParentDetailRepo _notificationParentDetailRepo;
        private readonly INotificationStaffDetailRepo _notificationStaffDetailRepo;
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
            IStaffRepository staffRepository,
            INotificationStaffDetailRepo notificationStaffDetailRepo)
        {
            _notificationdRepository = notificationdRepository;
            _notificationParentDetailRepo = notificationParentDetailRepo;
            _parentRepository = parentRepository;
            _mapper = mapper;
            _appSettings = option.CurrentValue;
            _httpContextAccessor = httpContextAccessor;
            _staffRepository = staffRepository;
            _notificationStaffDetailRepo = notificationStaffDetailRepo;
        }
        public void CreateNotification(NotificationDTO dto)
        {
            var entity = _mapper.Map<Notification>(dto);
            entity.CreatedAt = DateTime.Now;
            entity.Createddate = DateTime.Now;
            entity.IsDeleted = false;

            _notificationdRepository.AddAsync(entity).GetAwaiter().GetResult();
            _notificationdRepository.Save();
        }

        public void CreateNotificationForParent(NotificationDTO dto)
        {
            try
            {
                var parentEntities = _parentRepository.GetAll();
                var activeParents = parentEntities.Where(p => !p.IsDeleted).ToList();

                var notification = _mapper.Map<Notification>(dto);
                notification.CreatedAt = DateTime.Now;
                notification.Createddate = DateTime.Now;
                notification.IsDeleted = false;
                _notificationdRepository.Add(notification);
                _notificationdRepository.Save();


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
                        ModifiedDate = DateTime.Now,
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
            try
            {
                var staffs = _staffRepository.GetAll();
                var activeStaffs = staffs.Where(p => !p.IsDeleted).ToList();

                var notification = _mapper.Map<Notification>(dto);
                notification.CreatedAt = DateTime.Now;
                notification.Createddate = DateTime.Now;
                notification.IsDeleted = false;
                _notificationdRepository.Add(notification);
                _notificationdRepository.Save();

                foreach (var staff in activeStaffs)
                {
                    var detail = new Notificationstaffdetail
                    {
                        NotificationId = notification.NotificationId,
                        Staffid = staff.Staffid,
                        Message = dto.Message,
                        IsRead = false,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                        CreatedBy = notification.Createdby,
                        ModifiedDate = DateTime.Now,
                        ModifiedBy = notification.Modifiedby
                    };
                    _notificationStaffDetailRepo.Add(detail);
                }
                _notificationStaffDetailRepo.Save();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create notification for parents: {ex.Message}", ex);
            }
        }

        public void DeleteNotification(int id)
        {
            try
            {

                var entity = _notificationdRepository.GetByIdAsync(id).Result;
                if (entity != null)
                {
                    //                entity.IsDeleted = true; // Soft delete
                    _notificationdRepository.Delete(id);
                    _notificationdRepository.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete notification: {ex.Message}", ex);
            }
        }

        public List<Notification> GetAllNotifications()
        {
            var notifications = _notificationdRepository.GetAllAsync().Result;
            return notifications.Where(n => !n.IsDeleted).ToList();
        }

        public List<Notification> GetAllNotificationsForParent()
        {
            var details = _notificationParentDetailRepo.GetAll();
            var allNotifications = _notificationdRepository.GetAll();

            var notifications = (from d in details
                                 join n in allNotifications on d.NotificationId equals n.NotificationId
                                 where !d.IsDeleted && !n.IsDeleted
                                 select n)
                                .Distinct()
                                .ToList();

            return notifications;
        }

        public List<Notification> GetAllNotificationsForStaff()
        {
            var details = _notificationStaffDetailRepo.GetAll();
            var allNotifications = _notificationdRepository.GetAll();

            var notifications = (from d in details
                                 join n in allNotifications on d.NotificationId equals n.NotificationId
                                 where !d.IsDeleted && !n.IsDeleted
                                 select n)
                                .Distinct()
                                .ToList();

            return notifications;
        }
    }
}
