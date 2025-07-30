using BussinessLayer.IService;
using DataAccessLayer.DTO.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationDTO dto)
        {
            if (dto == null)
                return BadRequest("Notification data is null.");
            await _notificationService.CreateNotification(dto);
            return Ok("Notification created successfully.");
        }
        [HttpPost]
        [Route("createForParent")]
        public async Task<IActionResult> CreateNotificationForParent([FromBody] CreateNotificationDTO dto)
        {
            if (dto == null)
                return BadRequest("Notification data is null.");
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Message))
                return BadRequest("Title and content cannot be empty or whitespace.");
            try
            {
                await _notificationService.CreateNotificationForParent(dto);
                return Ok("Notification for parent created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating notification for parent: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("createForStaff")]
        public async Task<IActionResult> CreateNotificationForStaff([FromBody] CreateNotificationDTO dto)
        {
            if (dto == null)
                return BadRequest("Notification data is null.");
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Message))
                return BadRequest("Title and content cannot be empty or whitespace.");
            try
            {
                await _notificationService.CreateNotificationForStaff(dto);
                return Ok("Notification for staff created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating notification for staff: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("getAll")]
        public ActionResult<List<NotificationDTO>> GetAllNotifications()
        {
            var result = _notificationService.GetAllNotifications();
            return Ok(result);
        }
        [HttpGet]
        [Route("getNotiForParent")]
        public ActionResult<List<NotificationDTO>> GetNotificationsForParent()
        {
            var result = _notificationService.GetAllNotificationsForParent();
            return Ok(result);
        }
        [HttpGet]
        [Route("getNotiForParent/notread")]
        public ActionResult<List<NotificationDTO>> GetNotificationsForParentIsRead()
        {
            var result = _notificationService.GetAllNotificationsForParent().Where(n => !n.isRead);
            return Ok(result);
        }
        [HttpPut]
        [Route("updateIsReadForParent")]
        public ActionResult<List<NotificationDTO>> UpdateNotificationForParentRead()
        {
            var result = _notificationService.GetAllNotificationsForParent().Where(n => !n.isRead);
            foreach(var notification in result)
            {
                notification.isRead = true;
            }
            return Ok(result);

        }

        [HttpGet]
        [Route("getNotiForStaff")]
        public ActionResult<List<NotificationDTO>> GetNotificationsForStaff()
        {
            var result = _notificationService.GetAllNotificationsForStaff();
            return Ok(result);
        }
        [HttpGet]
        [Route("getNotiForStaff/notread")]
        public ActionResult<List<NotificationDTO>> GetNotificationsForStaffIsRead()
        {
            var result = _notificationService.GetAllNotificationsForStaff().Where(n => !n.isRead);
            return Ok(result);
        }
        [HttpPut]
        [Route("updateIsReadForStaff")]
        public ActionResult<List<NotificationDTO>> UpdateNotificationForStaffRead()
        {
            var result = _notificationService.GetAllNotificationsForStaff().Where(n => !n.isRead);
            foreach (var notification in result)
            {
                notification.isRead = true;
            }
            return Ok(result);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteNotification([FromBody] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid notification ID.");
            try
            {
                _notificationService.DeleteNotification(id);
                return Ok("Notification deleted successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Notification not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting notification: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("updateParentNoti/{id}")]
        public IActionResult UpdateNotificationForParent([FromBody] UpdateNotificationDTO dto, int id)
        {
            if (dto == null || id <= 0)
                return BadRequest("Invalid data.");
            try
            {
                _notificationService.UpdateNotificationForParent(dto, id);
                return Ok("Notification for parent updated successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Notification not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating notification for parent: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("updateStaffNoti/{id}")]
        public IActionResult UpdateNotificationForStaff([FromBody] UpdateNotificationDTO dto, int id)
        {
            if (dto == null || id <= 0)
                return BadRequest("Invalid data.");
            try
            {
                _notificationService.UpdateNotificationForStaff(dto, id);
                return Ok("Notification for staff updated successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Notification not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating notification for staff: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("updateIsRead/{notificationId}")]
        public async Task<IActionResult> UpdateNotificationIsReadAsync(int notificationId)
        {
            if (notificationId <= 0)
                return BadRequest("Invalid notification ID.");
            try
            {
                await _notificationService.UpdateNotificationIsReadAsync(notificationId);
                return Ok("Notification marked as read successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Notification not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating notification: {ex.Message}");
            }
        }
    }
}
