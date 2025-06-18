using BussinessLayer.IService;
using DataAccessLayer.DTO;
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
        public IActionResult CreateNotification([FromBody] NotificationDTO dto)
        {
            if (dto == null)
                return BadRequest("Notification data is null.");
            _notificationService.CreateNotification(dto);
            return Ok("Notification created successfully.");
        }
        [HttpPost]
        [Route("createForParent")]
        public IActionResult CreateNotificationForParent([FromBody] NotificationDTO dto)
        {
            if (dto == null)
                return BadRequest("Notification data is null.");
            _notificationService.CreateNotificationForParent(dto);
            return Ok("Notification for parent created successfully.");
        }
        [HttpGet]
        [Route("getAll")]
        public ActionResult<List<NotificationDTO>> GetAllNotifications()
        {
            var result = _notificationService.GetAllNotifications();
            return Ok(result);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteNotification(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid notification ID.");
            _notificationService.DeleteNotification(id);
            return Ok("Notification deleted successfully.");
        }
    }
}
