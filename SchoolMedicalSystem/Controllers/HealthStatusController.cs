using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO.HealthStatus;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthStatusController(IHealthStatusServices healthStatusServices,
        IMapper mapper) : ControllerBase
    {
        [HttpGet("healthstatuses")]
        public List<Healthstatus> GetHealthstatuses()
        {
            return healthStatusServices.GetHealthstatuses();
        }
        [HttpGet("healthstatus/{id}")]
        public ActionResult<Healthstatus> GetHealthstatusById(int id)
        {
            try
            {
                var healthStatus = healthStatusServices.GetHealthstatusByID(id);
                return Ok(healthStatus);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("healthstatus")]
        public ActionResult<Healthstatus> AddHealthStatus([FromBody] HealthStatusDTO healthstatus)
        {
            if (healthstatus == null)
            {
                return BadRequest("Health status cannot be null.");
            }
            try
            {
                var status = mapper.Map<Healthstatus>(healthstatus);
                healthStatusServices.AddHealthStatus(status);
                return CreatedAtAction(nameof(GetHealthstatusById), new { id = status.HealthId }, healthstatus);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("healthstatus/{id}")]
        public ActionResult<Healthstatus> UpdateHealthStatus(int id, [FromBody] HealthStatusDTO healthstatus)
        {
            if (healthstatus == null)
            {
                return BadRequest("Health status cannot be null.");
            }
            try
            {
                var existingHealthStatus = healthStatusServices.GetHealthstatusByID(id);
                if (existingHealthStatus == null)
                {
                    return NotFound($"Health status with ID {id} not found.");
                }
                var updatedStatus = mapper.Map<Healthstatus>(healthstatus);
                updatedStatus.HealthId = id; // Ensure the ID is set correctly
                healthStatusServices.UpdateHealthstatus(updatedStatus);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("healthstatus")]
        public ActionResult DeleteHealthStatus(int id)
        {
            try
            {
                var existingHealthStatus = healthStatusServices.GetHealthstatusByID(id);
                if (existingHealthStatus == null)
                {
                    return NotFound($"Health status with ID {id} not found.");
                }
                healthStatusServices.DeleteHealthstatus(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("healthstatuses/category/{categoryId}")]
        public ActionResult<List<Healthstatus>> GetHealthstatusesByCategoryId(int categoryId)
        {
            try
            {
                var healthStatuses = healthStatusServices.GetHealthstatusesByCategoryId(categoryId);
                if (healthStatuses == null || healthStatuses.Count == 0)
                {
                    return NotFound($"No health statuses found for category ID {categoryId}.");
                }
                return Ok(healthStatuses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("healthstatuses/student/{studentId}")]
        public ActionResult<List<Healthstatus>> GetHealthstatusesByStudentId(int studentId)
        {
            try
            {
                var healthStatuses = healthStatusServices.GetHealthstatusesByStudentId(studentId);
                if (healthStatuses == null || healthStatuses.Count == 0)
                {
                    return NotFound($"No health statuses found for student ID {studentId}.");
                }
                return Ok(healthStatuses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
