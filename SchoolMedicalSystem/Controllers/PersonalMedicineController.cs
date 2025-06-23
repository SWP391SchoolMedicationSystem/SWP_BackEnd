using BussinessLayer.IService;
using DataAccessLayer.DTO.PersonalMedicine;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalMedicineController : ControllerBase
    {
        private readonly IPersonalMedicineService _personalMedicineService;
        public PersonalMedicineController(IPersonalMedicineService personalMedicineService)
        {
            _personalMedicineService = personalMedicineService;
        }
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllPersonalMedicines()
        {
            var lists = await _personalMedicineService.GetAllPersonalMedicinesAsync();
            return Ok(lists);
        }
        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetPersonalMedicineById([FromQuery] int id)
        {
            try
            {
                var pm = await _personalMedicineService.GetPersonalMedicineById(id);
                return Ok(pm);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        [Route("add")]
        public IActionResult AddPersonalMedicine([FromBody] AddPersonalMedicineDTO personalMedicineDto)
        {
            if (personalMedicineDto == null)
                return BadRequest("Personal medicine data is null.");
            try
            {
                _personalMedicineService.AddPersonalMedicine(personalMedicineDto);
                return Ok("Personal medicine added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding personal medicine: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("update")]
        public IActionResult UpdatePersonalMedicine([FromBody] UpdatePersonalMedicineDTO personalMedicineDto)
        {
            if (personalMedicineDto == null)
                return BadRequest("Invalid data.");
            try
            {
                _personalMedicineService.UpdatePersonalMedicineAsync(personalMedicineDto);
                return Ok("Personal medicine updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating personal medicine: {ex.Message}");
            }
        }
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeletePersonalMedicine([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");
            try
            {
                _personalMedicineService.DeletePersonalMedicine(id);
                return Ok("Personal medicine deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting personal medicine: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("getAvailable")]
        public async Task<IActionResult> GetAvailablePersonalMedicines()
        {
            var lists = await _personalMedicineService.GetAvailablePersonalMedicineAsync();
            return Ok(lists);
        }
    }
}
