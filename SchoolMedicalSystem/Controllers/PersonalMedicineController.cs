using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.PersonalMedicine;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NPOI.XSSF.UserModel;
using static BussinessLayer.Utils.Constants;

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
            var Personalmedicines = await PersonalmedicineService.GetPersonalmedicineByIdAsync(id);
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
        [HttpPut("Personalmedicine/{id}")]

        public IActionResult UpdatePersonalmedicine([FromBody] UpdatePersonalMedicineDTO PersonalmedicineDto, int id)
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
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApprovePersonalMedicineRequest(ApprovalPersonalMedicineDTO dto, int id)
        {
            try{
                PersonalmedicineService.ApprovePersonalMedicine(dto, id);
                return Ok("Personal medicine request approved successfully.");
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> RejectPersonalMedicineRequest(ApprovalPersonalMedicineDTO dto, int id)
        {
            try
            {
                PersonalmedicineService.RejectPersonalMedicine(dto, id);
                return Ok("Personal medicine request rejected successfully.");
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
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
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingRequest()
        {
            var Personalmedicines = PersonalmedicineService.GetAllPersonalmedicinesAsync().Result.Where(p => p.DeliveryStatus == PersonalMedicineStatus.Pending);
            return Ok(Personalmedicines);
        }
        [HttpGet("approval")]

        public async Task<IActionResult> GetPersonalmedicinesByApproval()
        {
            var Personalmedicines = await PersonalmedicineService.GetPersonalmedicinesByApprovalAsync();
            return Ok(Personalmedicines);
        }


        [HttpGet("medicine/{medicineId}")]
        public async Task<IActionResult> GetPersonalmedicinesByMedicineId(string medicineName)
        {
            var Personalmedicines = await PersonalmedicineService.GetPersonalmedicinesByMedicineNameAsync(medicineName);
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
