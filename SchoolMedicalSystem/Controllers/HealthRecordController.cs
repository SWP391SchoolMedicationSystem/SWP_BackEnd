using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.HealthRecords;
using DataAccessLayer.Entity;
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
        [Route("healthrecord")]
        public async Task<IActionResult> AddHealthRecord([FromBody] CreateHealthRecordDTO dto)
        {
            if (dto == null)
                return BadRequest("Health record data is null.");
            if (dto.HealthRecordDate < DateTime.Now)
                return BadRequest("Date cannot be in the past.");

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
        [Route("getById")]
        public async Task<ActionResult<HealthRecordDto>> GetById([FromQuery] int id)
        {
            var result = await _healthRecordService.GetHealthRecordByIdAsync(id);
            if (result == null)
                return NotFound("Health record not found.");
            return Ok(result);
        }

        [HttpGet]
        [Route("getByStudentId")]
        public async Task<ActionResult<List<Healthrecord>>> GetByStudentId([FromQuery] int studentId)
        {
            var result = await _healthRecordService.GetHealthRecordsByStudentIdAsync(studentId);
            return Ok(result);
        }

        /*        public class HealthRecordContent
                {
                    public UpdateHealthRecordDTO dto { get; set; }
                    public int id { get; set; }
                }*/
        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] UpdateHealthRecordDTO content, [FromQuery] int id)
        {
            if (content == null)
                return BadRequest("Invalid data.");
            try
            {
                _healthRecordService.UpdateHealthRecord(content, id);
                return Ok("Health record updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating health record: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete([FromQuery] int id)
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
        [HttpGet]
        [Route("fullhealthrecord")]
        public async Task<ActionResult<List<HealthRecordStudentCheck>>> GetFullHealthRecords()
        {
            var result = await _healthRecordService.GetHealthRecords();
            return Ok(result);
        }
        [HttpGet]
        [Route("fullhealthrecordByStudentId")]
        public async Task<ActionResult<HealthRecordStudentCheck>> GetFullHealthRecordsByStudentId([FromQuery] int studentId)
        {
            var result = await _healthRecordService.GetHealthRecordsByStudentIdWithCheckAsync(studentId);
            if (result == null)
                return NotFound("Health record not found for the specified student.");
            return Ok(result);
        }
    }
}
