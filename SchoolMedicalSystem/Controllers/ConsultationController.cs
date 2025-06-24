using BussinessLayer.IService;
using BussinessLayer.Service;
using DataAccessLayer.DTO.Consultation;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationController(IConsultationService requestservice, IConsultationTypeService typeservice) : ControllerBase
    {
        [HttpGet("consultationrequests")]
        public async Task<IActionResult> GetAllConsultationRequests()
        {
            var requests = await requestservice.GetAllConsultationRequestsAsync();
            return Ok(requests);
        }
        [HttpGet("consultationtypes")]
        public async Task<IActionResult> GetAllConsultationTypes()
        {
            var types = await typeservice.GetAllConsultationTypesAsync();
            return Ok(types);
        }
        [HttpPost("consultationrequest")]
        public async Task<IActionResult> AddConsultationRequest([FromBody] ConsultationRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }
            var createdRequest = await requestservice.AddConsultationRequest(request);
            return CreatedAtAction(nameof(GetAllConsultationRequests), new { id = createdRequest.Consultationid }, createdRequest);
        }
        [HttpPost("consultationtype")]
        public async Task<IActionResult> AddConsultationType([FromBody] ConsultationTypeDTO consultationType)
        {
            if (consultationType == null)
            {
                return BadRequest("Invalid consultation type data.");
            }
            var createdType = await typeservice.AddConsultationTypeAsync(consultationType);
            return CreatedAtAction(nameof(GetAllConsultationTypes), new { id = createdType.Typeid }, createdType);
        }
        [HttpPut("consultationrequest")]
        public async Task<IActionResult> UpdateConsultationRequest([FromBody] Consultationrequest request)
        {
            if (request == null || request.Consultationid <= 0)
            {
                return BadRequest("Invalid request data.");
            }
            var updatedRequest = await requestservice.UpdateConsulationRequest(request);
            if (updatedRequest == null)
            {
                return NotFound("Consultation request not found.");
            }
            return Ok(updatedRequest);
        }
        [HttpPut("consultationtype")]
        public async Task<IActionResult> UpdateConsultationType([FromBody] Consultationtype consultationType)
        {
            if (consultationType == null || consultationType.Typeid <= 0)
            {
                return BadRequest("Invalid consultation type data.");
            }
            var updatedType = await typeservice.UpdateConsultationTypeAsync(consultationType);
            if (updatedType == null)
            {
                return NotFound("Consultation type not found.");
            }
            return Ok(updatedType);
        }
        [HttpDelete("consultationrequest/{id}")]
        public IActionResult DeleteConsultationRequest(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid request ID.");
            }
            requestservice.DeleteConsultationRequest(id);
            return NoContent();
        }
        [HttpDelete("consultationtype/{id}")]
        public IActionResult DeleteConsultationType(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid consultation type ID.");
            }
            typeservice.DeleteConsultationType(id);
            return NoContent();
        }
    }
}
