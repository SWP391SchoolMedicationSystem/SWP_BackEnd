using BussinessLayer.IService;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationRecordController : ControllerBase
    {
        private readonly IVaccinationRecordService _vaccinationRecordService;
        public VaccinationRecordController(IVaccinationRecordService vaccinationRecordService)
        {
            _vaccinationRecordService = vaccinationRecordService;
        }
        [HttpGet]
        [Route("vaccinationrecord")]
        public async Task<IActionResult> GetAllVaccinationRecords()
        {
            var records = await _vaccinationRecordService.GetAllVaccinationRecords();
            return Ok(records);
        }
        [HttpPost]
        [Route("vaccinationrecord")]
        public async Task<IActionResult> AddVaccinationRecord([FromBody] VaccinationRecordDTO record)
        {
            if (record == null)
            {
                return BadRequest("Invalid vaccination record data.");
            }
            await _vaccinationRecordService.AddVaccinationRecordAsync(record);
            return CreatedAtAction(nameof(GetAllVaccinationRecords), new { id = record.Vaccinename }, record);
        }
        [HttpDelete]
        [Route("vaccinationrecord/{id}")]
        public async Task<IActionResult> DeleteVaccinationRecord(int id)
        {
            try
            {
                _vaccinationRecordService.DeleteVaccinationRecord(id);
                return Ok("Deleted successfully");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut]
        [Route("vaccinationrecord")]
        public IActionResult UpdateVaccinationRecord([FromBody] VaccinationRecordDTO record)
        {
            if (record == null)
            {
                return BadRequest("Invalid vaccination record data.");
            }
            _vaccinationRecordService.UpdateVaccinationRecord(record);
            return Ok("Updated successfully");
        }

    }
}
