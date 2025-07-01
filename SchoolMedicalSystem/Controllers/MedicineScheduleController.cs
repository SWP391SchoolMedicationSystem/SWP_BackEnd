using BussinessLayer.IService;
using DataAccessLayer.DTO.Medicines;
using DataAccessLayer.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineScheduleController(IMedicineScheduleService medicineScheduleService) : ControllerBase
    {
        [HttpGet]
        [Route("medicineschedule")]
        public async Task<IActionResult> GetAllMedicineSchedules()
        {
            try
            {
                var schedules = await medicineScheduleService.GetAllMedicineSchedulesAsync();
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("medicineschedule/{id}")]
        public
            async Task<IActionResult> GetMedicineScheduleById(int id)
        {
            try
            {
                var schedule = await medicineScheduleService.GetMedicineScheduleByIdAsync(id);
                if (schedule == null)
                {
                    return NotFound($"Medicine schedule with ID {id} not found.");
                }
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("medicineschedule")]
        public async Task<IActionResult> AddMedicineSchedule([FromBody] AddScheduleMedicineDTO medicineSchedule)
        {
            if (medicineSchedule == null)
            {
                return BadRequest("Medicine schedule data is null.");
            }
            try
            {
                await medicineScheduleService.AddMedicineScheduleAsync(medicineSchedule);
                return CreatedAtAction(nameof(GetMedicineScheduleById), medicineSchedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("medicineschedule")]
        public
            async Task<IActionResult> UpdateMedicineSchedule(int id, [FromBody] MedicineScheduleDTO medicineSchedule)
        {
            if (medicineSchedule == null)
            {
                return BadRequest("Medicine schedule data is null or ID mismatch.");
            }
            try
            {
                await medicineScheduleService.UpdateMedicineScheduleAsync(medicineSchedule);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete]
        [Route("medicineschedule/{id}")]
        public async Task<IActionResult> DeleteMedicineSchedule(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid medicine schedule ID.");
            }
            try
            {
                await medicineScheduleService.DeleteMedicineScheduleAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
