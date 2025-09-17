using BussinessLayer.IService;
using DataAccessLayer.DTO.Medicines;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
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
        public async Task<IActionResult> AddMedicine([FromBody] CreateMedicineDTO medicineDto)
        {
            if (medicineDto == null)
                return BadRequest("Medicine data is null.");
            try
            {
                await _medicineService.AddMedicine(medicineDto);
                return Ok("Medicine added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding medicine: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("UpdateMedicine")]
        public async Task<IActionResult> UpdateMedicine([FromBody] UpdateMedicineDTO medicineDto)
        {
            if (medicineDto == null)
                return BadRequest("Invalid data.");
            try
            {
                await _medicineService.UpdateMedicine(medicineDto);
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
        [HttpDelete]
        [Route("DeleteMedicine")]
        public async Task<IActionResult> DeleteMedicine([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid medicine ID.");
            try
            {
                await _medicineService.DeleteMedicine(id);
                return Ok("Medicine deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting medicine: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetAvailableMedicines")]
        public async Task<IActionResult> GetAvailableMedicines()
        {
            try
            {
                var medicines = await _medicineService.GetAvailableMedicinesAsync();
                return Ok(medicines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
