using BussinessLayer.IService;
using DataAccessLayer.DTO.Medicines;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }
        [HttpGet]
        [Route("GetAllMedicines")]
        public async Task<IActionResult> GetAllMedicines()
        {
            var medicines = await _medicineService.GetAllMedicinesAsync();
            return Ok(medicines);
        }
        [HttpGet]
        [Route("GetMedicineById")]
        public async Task<IActionResult> GetMedicineById([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid medicine ID.");
            try
            {
                var medicine = await _medicineService.GetMedicineByIdAsync(id);
                if (medicine == null)
                    return NotFound("Medicine not found.");
                return Ok(medicine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpPost]
        [Route("AddMedicine")]
        public IActionResult AddMedicine([FromBody] CreateMedicineDTO medicineDto)
        {
            if (medicineDto == null)
                return BadRequest("Medicine data is null.");
            try
            {
                _medicineService.AddMedicine(medicineDto);
                return Ok("Medicine added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding medicine: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("UpdateMedicine")]
        public IActionResult UpdateMedicine([FromBody] UpdateMedicineDTO medicineDto)
        {
            if (medicineDto == null)
                return BadRequest("Invalid data.");
            try
            {
                _medicineService.UpdateMedicine(medicineDto);
                return Ok("Medicine updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating medicine: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("SearchMedicinesByName")]
        public async Task<IActionResult> SearchMedicinesByName([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return BadRequest("Search term cannot be empty.");
            var medicines = await _medicineService.SearchMedicinesNameAsync(searchTerm);
            return Ok(medicines);
        }
        [HttpGet]
        [Route("GetMedicinesByCategoryId")]
        public async Task<IActionResult> GetMedicinesByCategoryId([FromQuery] int categoryId)
        {
            var medicines = await _medicineService.GetMedicinesByCategoryIdAsync(categoryId);
            if (medicines == null || !medicines.Any())
                return NotFound("No medicines found for the specified category.");
            return Ok(medicines);
        }
    }
}
