using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotiParentDetailController : ControllerBase
    {
        private readonly INotifcationParentDetailService _parentDetailService;
        private readonly IMapper _mapper;
        private readonly IParentService _parentService;
        public NotiParentDetailController(INotifcationParentDetailService parentDetailService,
            IMapper mapper, IParentService parentService)
        {
            _parentDetailService = parentDetailService;
            _mapper = mapper;
            _parentService = parentService;
        }
        [HttpPost("AddNotificationParentDetail")]
        public async Task<IActionResult> AddNotificationParentDetail([FromBody] NotiParentDetailDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Notification data is null.");
                }

                // Check if parent exists
                var parent = await _parentService.GetParentByIdAsync(dto.ParentId);
                if (parent == null)
                {
                    return NotFound($"Parent with id {dto.ParentId} not found.");
                }
                _parentDetailService.AddNotificationParentDetail(dto);
                return Ok("Notification added successfully.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
