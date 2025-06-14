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
        public async Task<IActionResult> AddHealthRecord([FromBody] HealthRecordDTO dto)
        {
            if (dto == null)
                return BadRequest("Health record data is null.");

            await _healthRecordService.AddHealthRecordAsync(dto);
            return Ok("Health record added successfully.");
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
        [Route("update")]
        public IActionResult Update([FromBody] HealthRecordDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            _healthRecordService.UpdateHealthRecord(dto);
            return Ok("Health record updated.");
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _healthRecordService.DeleteHealthRecord(id);
            return Ok("Health record deleted.");
        }
    }
}
