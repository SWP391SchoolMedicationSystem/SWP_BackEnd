using BussinessLayer.IService;
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
            return Ok("Get all personal medicines");
        }
        [HttpGet]
        [Route("getById")]
        public IActionResult GetPersonalMedicineById([FromQuery] int id)
        {
            // Logic to get personal medicine by ID
            return Ok($"Get personal medicine by ID: {id}");
        }
        [HttpPost]
        [Route("add")]
        public IActionResult AddPersonalMedicine([FromBody] object personalMedicineDto)
        {
            // Logic to add personal medicine
            if (personalMedicineDto == null)
                return BadRequest("Personal medicine data is null.");
            return Ok("Add personal medicine");
        }
        [HttpPut]
        [Route("update")]
        public IActionResult UpdatePersonalMedicine([FromBody] object personalMedicineDto)
        {
            // Logic to update personal medicine
            if (personalMedicineDto == null)
                return BadRequest("Invalid data.");
            return Ok("Update personal medicine");
        }
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeletePersonalMedicine([FromQuery] int id)
        {
            // Logic to delete personal medicine
            if (id <= 0)
                return BadRequest("Invalid ID.");
            return Ok($"Delete personal medicine with ID: {id}");
        }
        [HttpGet]
        [Route("search")]
        public IActionResult SearchPersonalMedicinesByMedicineName([FromQuery] string searchTerm)
        {
            // Logic to search personal medicines by medicine name
            if (string.IsNullOrEmpty(searchTerm))
                return BadRequest("Search term cannot be empty.");
            return Ok($"Search personal medicines by medicine name: {searchTerm}");
        }
    }
}
