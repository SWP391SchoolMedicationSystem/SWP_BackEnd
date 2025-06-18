using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthRecordController : ControllerBase
    {
        private readonly IHealthRecordService _healthRecordService;

        public HealthRecordController(IHealthRecordService healthRecordService)
        {
            _healthRecordService = healthRecordService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddHealthRecord([FromBody] CreateHealthRecordDTO dto)
        {
            if (dto == null)
                return BadRequest("Health record data is null.");
            try
            {
                await _healthRecordService.AddHealthRecordAsync(dto);
                return Ok("Health record added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Can't add health record: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<List<Healthrecord>>> GetAll()
        {
            var result = await _healthRecordService.GetAllHealthRecordsAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<ActionResult<HealthRecordDTO>> GetById(int id)
        {
            var result = await _healthRecordService.GetHealthRecordByIdAsync(id);
            if (result == null)
                return NotFound("Health record not found.");
            return Ok(result);
        }

        [HttpGet]
        [Route("getByStudentId/{studentId}")]
        public async Task<ActionResult<List<Healthrecord>>> GetByStudentId(int studentId)
        {
            var result = await _healthRecordService.GetHealthRecordsByStudentIdAsync(studentId);
            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update([FromBody] UpdateHealthRecordDTO dto, int id)
        {
            if (dto == null)
                return BadRequest("Invalid data.");
            try
            {
                _healthRecordService.UpdateHealthRecord(dto, id);
                return Ok("Health record updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating health record: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid health record ID.");
            try
            {
                _healthRecordService.DeleteHealthRecord(id);
                return Ok("Health record deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting health record: {ex.Message}");
            }
        }
    }
}
