using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.QuartzJob.Scheduler;
using DataAccessLayer.DTO;
using DataAccessLayer.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleDetailController(IScheduleDetailService scheduleDetailService, NotifyScheduler scheduler, IMedicineScheduleRepository medicineScheduleRepository) : ControllerBase
    {
        [HttpGet]
        [Route("scheduledetails")]
        public async Task<IActionResult> GetAllScheduleDetails()
        {
            var scheduleDetails = await scheduleDetailService.GetAllScheduleDetailsAsync();
            return Ok(scheduleDetails);
        }
        [HttpGet]
        [Route("scheduledetail/{id}")]
        public async Task<IActionResult> GetScheduleDetailById(int id)
        {
            var scheduleDetail = await scheduleDetailService.GetScheduleDetailByIdAsync(id);
            if (scheduleDetail == null)
            {
                return NotFound();
            }
            return Ok(scheduleDetail);
        }
        [HttpPost]
        [Route("scheduledetail")]
        public async Task<IActionResult> AddScheduleDetail([FromBody] ScheduleDetailDTO scheduleDetail)
        {
            if (scheduleDetail == null)
            {
                return BadRequest("Schedule detail cannot be null");
            }
            await scheduleDetailService.AddScheduleDetailAsync(scheduleDetail);
            return Ok("Added successfully");
        }
        [HttpPut]
        [Route("scheduledetail/{id}")]
        public async Task<IActionResult> UpdateScheduleDetail(int id, [FromBody] ScheduleDetailDTO scheduleDetail)
        {
            if (scheduleDetail == null)
            {
                return BadRequest("Invalid schedule detail data");
            }
            await scheduleDetailService.UpdateScheduleDetailAsync(scheduleDetail, id);
            return NoContent();
        }
        [HttpDelete]
        [Route("scheduledetail/{id}")]
        public async Task<IActionResult> DeleteScheduleDetail(int id)
        {
            var scheduleDetail = await scheduleDetailService.GetScheduleDetailByIdAsync(id);
            if (scheduleDetail == null)
            {
                return NotFound();
            }
            await scheduleDetailService.DeleteScheduleDetailAsync(id);
            return NoContent();
        }

        [HttpPost]
        [Route("sendschedule")]
        public async Task<IActionResult> SendSchedule()
        {
            var scheduleDetails = await medicineScheduleRepository.GetAllAsync();
            await scheduler.ScheduleMedicalNotifyJob(scheduleDetails);
            if (scheduleDetails == null || !scheduleDetails.Any())
            {
                return NotFound($"No schedule details found.");
            }
            return Ok("Success");
        }
    }
}
