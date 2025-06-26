using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineDonationController(IMedicineDonationService medicineDonationService, IMapper mapper) : ControllerBase
    {
        [HttpGet("medicinedonations")]
        public async Task<IActionResult> GetAllMedicineDonations()
        {
            var medicineDonations = await medicineDonationService.GetAllMedicineDonationsAsync();
            return Ok(medicineDonations);
        }
        [HttpGet("medicinedonation/{id}")]
        public async Task<IActionResult> GetMedicineDonationById(int id)
        {
            var medicineDonation = await medicineDonationService.GetMedicineDonationByIdAsync(id);
            if (medicineDonation == null)
                return NotFound("Medicine donation not found.");
            return Ok(medicineDonation);
        }
        [HttpPost("medicinedonation")]
        public async Task<IActionResult> AddMedicineDonation([FromBody] MedicineDonationDto medicineDonationDto)
        {
            if (medicineDonationDto == null)
                return BadRequest("Medicine donation data is null.");
            try
            {
                await medicineDonationService.AddMedicineDonationAsync(medicineDonationDto);
                return Ok("Medicine donation added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding medicine donation: {ex.Message}");
            }
        }
        [HttpPut("medicinedonation")]
        public IActionResult UpdateMedicineDonation(int id, [FromBody] MedicineDonationDto medicineDonationDto)
        {
            if (medicineDonationDto == null)
                return BadRequest("Invalid data.");
            try
            {
                medicineDonationService.UpdateMedicineDonation(id, medicineDonationDto);
                return Ok("Medicine donation updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating medicine donation: {ex.Message}");
            }
        }
        [HttpDelete("medicinedonation/{id}")]
        public IActionResult DeleteMedicineDonation(int id)
        {
            try
            {
                medicineDonationService.DeleteMedicineDonation(id);
                return Ok("Medicine donation deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting medicine donation: {ex.Message}");
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchMedicineDonations([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return BadRequest("Search term cannot be empty.");
            var medicineDonations = await medicineDonationService.SearchMedicineDonationsAsync(searchTerm);
            return Ok(medicineDonations);
        }
        [HttpGet("parent/{parentId}")]
        public async Task<IActionResult> GetMedicineDonationsByParentId(int parentId)
        {
            var medicineDonations = await medicineDonationService.GetMedicineDonationsByParentIdAsync(parentId);
            return Ok(medicineDonations);
        }
        [HttpGet("medicine/{medicineId}")]
        public async Task<IActionResult> GetMedicineDonationsByMedicineId(int medicineId)
        {
            var medicineDonations = await medicineDonationService.GetMedicineDonationsByMedicineIdAsync(medicineId);
            return Ok(medicineDonations);

        }

    }
}
