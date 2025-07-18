using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.Hubs;
using BussinessLayer.IService;
using BussinessLayer.Utils.Configurations;
using DataAccessLayer.DTO.Notifications;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IHubContext<NotificationHub> _hub;
        public NotificationService(
            INotificationRepo notificationdRepository,
            INotificationParentDetailRepo notificationParentDetailRepo,
            IParentRepository parentRepository,
            IMapper mapper,
            IOptionsMonitor<AppSetting> option,
            IHttpContextAccessor httpContextAccessor,
            IStaffRepository staffRepository,
            INotificationStaffDetailRepo notificationStaffDetailRepo,
            IHubContext<NotificationHub> hub)
        {
            _notificationdRepository = notificationdRepository;
            _notificationParentDetailRepo = notificationParentDetailRepo;
            _parentRepository = parentRepository;
            _mapper = mapper;
            _appSettings = option.CurrentValue;
            _httpContextAccessor = httpContextAccessor;
            _staffRepository = staffRepository;
            _notificationStaffDetailRepo = notificationStaffDetailRepo;
            _hub = hub;
        }
        const string parentTarget = "Parents";
        const string staffTarget = "Staffs";
        public async Task CreateNotification(CreateNotificationDTO dto)
        {
            try
            {
                var entity = _mapper.Map<Notification>(dto);
                entity.CreatedAt = DateTime.Now;
                entity.Createddate = DateTime.Now;
                entity.IsDeleted = false;
                _notificationdRepository.AddAsync(entity);
                _notificationdRepository.Save();
                var parentEntities = _parentRepository.GetAll();
                var activeParents = parentEntities.Where(p => !p.IsDeleted).ToList();
                foreach (var parent in activeParents)
                {
                    var detail = new NotificationParentDetail
                    {
                        NotificationId = entity.NotificationId,
                        ParentId = parent.Parentid,
                        Message = dto.Message,
                        IsRead = false,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                        CreatedBy = entity.Createdby,
                        //                        ModifiedDate = DateTime.Now,
                        //                        ModifiedBy = notification.Modifiedby
                    };
                    _notificationParentDetailRepo.Add(detail);
                }
                _notificationParentDetailRepo.Save();

                var staffs = _staffRepository.GetAll();
                var activeStaffs = staffs.Where(p => !p.IsDeleted).ToList();
                foreach (var staff in activeStaffs)
                {
                    var detail = new Notificationstaffdetail
                    {
                        NotificationId = entity.NotificationId,
                        Staffid = staff.Staffid,
                        Message = dto.Message,
                        IsRead = false,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                        CreatedBy = entity.Createdby,
                        //                        ModifiedDate = DateTime.Now,
                        //                        ModifiedBy = notification.Modifiedby
                    };
                    _notificationStaffDetailRepo.Add(detail);
                }
                _notificationStaffDetailRepo.Save();
                await _hub.Clients.All.SendAsync("ReceiveNotification", new
                {
                    dto.Title,
                    dto.Message,
                    Target = "All"
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create notification: {ex.Message}", ex);
            }
        }

        public async Task CreateNotificationForParent(CreateNotificationDTO dto)
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
                        //                        ModifiedDate = DateTime.Now,
                        //                        ModifiedBy = notification.Modifiedby
                    };
                    _notificationParentDetailRepo.Add(detail);
                }
                _notificationParentDetailRepo.Save();
                await _hub.Clients.Group(parentTarget).SendAsync("ReceiveNotification", new
                {
                    dto.Title,
                    dto.Message,
                    Target = parentTarget
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create notification for parents: {ex.Message}", ex);
            }
        }

        public async Task CreateNotificationForStaff(CreateNotificationDTO dto)
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
                        //                        ModifiedDate = DateTime.Now,
                        //                        ModifiedBy = notification.Modifiedby
                    };
                    _notificationStaffDetailRepo.Add(detail);
                }
                _notificationStaffDetailRepo.Save();
                await _hub.Clients.Group(staffTarget).SendAsync("ReceiveNotification", new
                {
                    dto.Title,
                    dto.Message,
                    Target = staffTarget
                });
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
                    entity.IsDeleted = true; // Soft delete
                    _notificationdRepository.Update(entity);
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

        public void UpdateNotificationForParent(UpdateNotificationDTO dto, int notificationId)
        {
            try
            {
                var notification = _notificationdRepository.GetByIdAsync(notificationId).Result;
                if (notification == null || notification.IsDeleted == true)
                {
                    throw new Exception("Notification not found or has been deleted.");
                }
                notification.Title = dto.Title;
                notification.Type = dto.Type;
                notification.Modifieddate = DateTime.Now;
                notification.Modifiedby = dto.Modifiedby;

                _notificationdRepository.Update(notification);
                _notificationdRepository.Save();

                var activeParents = _parentRepository.GetAll().Where(p => !p.IsDeleted).ToList();

                var existingDetails = _notificationParentDetailRepo
                    .GetAll()
                    .Where(d => d.NotificationId == notificationId)
                    .ToList();

                foreach (var parent in activeParents)
                {
                    var existingDetail = existingDetails.FirstOrDefault(d => d.ParentId == parent.Parentid);

                    if (existingDetail != null)
                    {
                        existingDetail.Message = dto.Message;
                        existingDetail.ModifiedDate = DateTime.Now;
                        existingDetail.ModifiedBy = dto.Modifiedby;

                        _notificationParentDetailRepo.Update(existingDetail);
                    }
                    else
                    {
                        //add new detail if not present
                        var newDetail = new NotificationParentDetail
                        {
                            NotificationId = notification.NotificationId,
                            ParentId = parent.Parentid,
                            Message = dto.Message,
                            IsRead = false,
                            IsDeleted = false,
                            CreatedDate = DateTime.Now,
                            CreatedBy = notification.Createdby,
                            ModifiedDate = DateTime.Now,
                            ModifiedBy = dto.Modifiedby
                        };

                        _notificationParentDetailRepo.Add(newDetail);
                    }
                }

                _notificationParentDetailRepo.Save();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update notification for parents: {ex.Message}", ex);
            }
        }

        public void UpdateNotificationForStaff(UpdateNotificationDTO dto, int id)
        {
            try
            {
                var notification = _notificationdRepository.GetByIdAsync(id).Result;
                if (notification == null || notification.IsDeleted == true)
                {
                    throw new Exception("Notification not found or has been deleted.");
                }
                notification.Title = dto.Title;
                notification.Type = dto.Type;
                notification.Modifieddate = DateTime.Now;
                notification.Modifiedby = dto.Modifiedby;

                _notificationdRepository.Update(notification);
                _notificationdRepository.Save();

                var activeStaffs = _staffRepository.GetAll().Where(p => !p.IsDeleted).ToList();

                var existingDetails = _notificationStaffDetailRepo
                    .GetAll()
                    .Where(d => d.NotificationId == id)
                    .ToList();

                foreach (var staff in activeStaffs)
                {
                    var existingDetail = existingDetails.FirstOrDefault(d => d.Staffid == staff.Staffid);

                    if (existingDetail != null)
                    {
                        existingDetail.Message = dto.Message;

                        existingDetail.ModifiedDate = DateTime.Now;
                        existingDetail.ModifiedBy = dto.Modifiedby;

                        _notificationStaffDetailRepo.Update(existingDetail);
                    }
                    else
                    {
                        //add new detail if not present
                        var newDetail = new Notificationstaffdetail
                        {
                            NotificationId = notification.NotificationId,
                            Staffid = staff.Staffid,
                            Message = dto.Message,
                            IsRead = false,
                            IsDeleted = false,
                            CreatedDate = DateTime.Now,
                            CreatedBy = notification.Createdby,
                            ModifiedDate = DateTime.Now,
                            ModifiedBy = dto.Modifiedby
                        };

                        _notificationStaffDetailRepo.Add(newDetail);
                    }
                }

                _notificationParentDetailRepo.Save();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update notification for parents: {ex.Message}", ex);
            }
        }

        public async Task UpdateNotificationIsReadAsync(int notificationId)
        {
            try
            {
                var detail = _notificationdRepository.GetAllAsync().Result.FirstOrDefault(n => n.NotificationId == notificationId);
                if (detail != null)
                {
                    detail.isRead = true;
                    _notificationdRepository.Update(detail);
                    _notificationdRepository.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update notification read status: {ex.Message}", ex);
            }
        }
    }
}
