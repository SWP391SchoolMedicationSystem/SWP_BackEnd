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
            if (PersonalmedicineDto.ExpiryDate < DateTime.Now)
                return BadRequest("Date cannot be in the past.");
            if (PersonalmedicineDto.Receiveddate < DateTime.Now)
                return BadRequest("Date cannot be in the past.");


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

        public async Task<IActionResult> UpdatePersonalmedicine([FromBody] UpdatePersonalMedicineDTO PersonalmedicineDto)
        {
            if (PersonalmedicineDto == null)
                return BadRequest("Invalid data.");
            try
            {
                await PersonalmedicineService.UpdatePersonalmedicine(PersonalmedicineDto);
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

    }
}
