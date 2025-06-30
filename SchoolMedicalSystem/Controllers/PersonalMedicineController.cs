using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.PersonalMedicine;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NPOI.XSSF.UserModel;

namespace SchoolMedicalSystem.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class PersonalMedicineController(IPersonalmedicineService PersonalmedicineService, IMapper mapper) : ControllerBase
    {
        [HttpGet("Personalmedicines")]
        public async Task<IActionResult> GetAllPersonalmedicines()
        {
            var Personalmedicines = await PersonalmedicineService.GetAllPersonalmedicinesAsync();
            return Ok(Personalmedicines);
        }
        [HttpGet("Personalmedicine/{id}")]
        public async Task<IActionResult> GetPersonalmedicineById(int id)
        {
            var Personalmedicines = await PersonalmedicineService.GetPersonalmedicinesByMedicineIdAsync(id);
            if (Personalmedicines == null)
                return NotFound("Medicine donation not found.");
            return Ok(Personalmedicines);
        }
        [HttpPost("Personalmedicine")]
        public async Task<IActionResult> AddPersonalmedicine([FromBody] AddPersonalMedicineDTO PersonalmedicineDto)
        {
            if (PersonalmedicineDto == null)
                return BadRequest("Medicine donation data is null.");
            try
            {
                await PersonalmedicineService.AddPersonalmedicineAsync(PersonalmedicineDto);
                return Ok("Medicine donation added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding medicine donation: {ex.Message}");
            }
        }
        [HttpPut("Personalmedicine")]
        public IActionResult UpdatePersonalmedicine([FromBody] int id,UpdatePersonalMedicineDTO PersonalmedicineDto)
        {
            if (PersonalmedicineDto == null)
                return BadRequest("Invalid data.");
            try
            {
                PersonalmedicineService.UpdatePersonalmedicine(PersonalmedicineDto, id);
                return Ok("Medicine donation updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating medicine donation: {ex.Message}");
            }
        }
        [HttpDelete("Personalmedicine/{id}")]
        public IActionResult DeletePersonalmedicine(int id)
        {
            try
            {
                PersonalmedicineService.DeletePersonalmedicine(id);
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
        public async Task<IActionResult> SearchPersonalmedicines([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return BadRequest("Search term cannot be empty.");
            var Personalmedicines = await PersonalmedicineService.SearchPersonalmedicinesAsync(searchTerm);
            return Ok(Personalmedicines);
        }
        [HttpGet("parent/{parentId}")]
        public async Task<IActionResult> GetPersonalmedicinesByParentId(int parentId)
        {
            var Personalmedicines = await PersonalmedicineService.GetPersonalmedicinesByParentIdAsync(parentId);
            return Ok(Personalmedicines);
        }
        [HttpGet("medicine/{medicineId}")]
        public async Task<IActionResult> GetPersonalmedicinesByMedicineId(int medicineId)
        {
            var Personalmedicines = await PersonalmedicineService.GetPersonalmedicinesByMedicineIdAsync(medicineId);
            return Ok(Personalmedicines);

        }
        [HttpGet("approval/{isApproved}")]
        public async Task<IActionResult> GetPersonalmedicinesByApproval(int isApproved)
        {
            var Personalmedicines = await PersonalmedicineService.GetPersonalmedicinesByApprovalAsync(isApproved);
            return Ok(Personalmedicines);
        }
        [HttpGet("requests")]
        public async Task<ActionResult<List<PersonalMedicineRequestDTO>>> GetRequest()
        {
            try
            {
                var requests = await PersonalmedicineService.GetRequest();
                if (requests == null || !requests.Any())
                    return NotFound("No requests found.");
                return new JsonResult(new
                {
                    status = "success",
                    result = requests
                });


            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }

        }
    }
}
