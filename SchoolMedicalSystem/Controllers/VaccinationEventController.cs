using BussinessLayer.IService;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationEventController : ControllerBase
    { 
        private readonly IVaccinationEventService _vaccinationEventService;
        public VaccinationEventController(IVaccinationEventService vaccinationEventService)
        {
            _vaccinationEventService = vaccinationEventService;
        }
        [HttpGet]
        [Route("vaccinationevent")]
        public async Task<IActionResult> GetAllVaccinationEvents()
        {
            var events = await _vaccinationEventService.GetAllVaccinationEvents();
            return Ok(events);
        }
        [HttpPost]
        [Route("vaccinationevent")]
       public IActionResult AddVaccinationEvent([FromBody] VaccinationEventDTO dto)
        {
            if (dto == null)
            {
                return Ok("No object");
            }
            _vaccinationEventService.AddVaccinationEventAsync(dto);
            return CreatedAtAction(nameof(GetAllVaccinationEvents), new { id = dto.Vaccinationeventname }, dto);
        }
        [HttpDelete]
        [Route("vaccinationevent/{id}")]
        public async Task<IActionResult> DeleteVaccinationEvent(int id)
        {
            try
            {
                _vaccinationEventService.DeleteVaccinationEvent(id);
                return Ok("Deleted successfully");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut]
        [Route("vaccinationevent")]
        public IActionResult UpdateVaccinationEvent([FromBody] VaccinationEventDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid vaccination event data.");
            }
            _vaccinationEventService.UpdateVaccinationEvent(dto);
            return Ok("Updated successfully");
        }
    }
}
