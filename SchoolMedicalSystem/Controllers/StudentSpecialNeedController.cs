using BussinessLayer.IService;
using DataAccessLayer.DTO.StudentSpecialNeeds;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSpecialNeedController : ControllerBase
    {
        private readonly IStudentSpecialNeedService _studentSpecialNeedService;
        public StudentSpecialNeedController(IStudentSpecialNeedService studentSpecialNeedService)
        {
            _studentSpecialNeedService = studentSpecialNeedService;
        }
        [HttpGet]
        [Route("GetAllSpecialNeeds")]
        public async Task<IActionResult> GetAllSpecialNeeds()
        {
            var specialNeeds = await _studentSpecialNeedService.GetAllStudentSpecialNeedsAsync();
            return Ok(specialNeeds);
        }
        [HttpGet]
        [Route("GetSpecialNeedById")]
        public async Task<IActionResult> GetSpecialNeedById([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid special need ID.");
            try
            {
                var specialNeed = await _studentSpecialNeedService.GetStudentSpecialNeedByIdAsync(id);
                if (specialNeed == null)
                    return NotFound("Special need not found.");
                return Ok(specialNeed);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("AddSpecialNeed")]
        public IActionResult AddSpecialNeed([FromBody] CreateSpecialStudentNeedDTO specialNeedDto)
        {
            if (specialNeedDto == null)
                return BadRequest("Special need data is null.");
            try
            {
                _studentSpecialNeedService.AddStudentSpecialNeed(specialNeedDto);
                return Ok("Special need added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding special need: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("UpdateSpecialNeed")]
        public IActionResult UpdateSpecialNeed([FromBody] UpdateStudentSpecialNeedDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");
            try
            {
                _studentSpecialNeedService.UpdateStudentSpecialNeed(dto);
                return Ok("Special need updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating special need: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetSpecialNeedByStudentId")]
        public async Task<IActionResult> GetSpecialNeedByStudentId([FromQuery] int studentId)
        {
            if (studentId <= 0)
                return BadRequest("Invalid student ID.");
            try
            {
                var specialNeeds = await _studentSpecialNeedService.GetStudentSpecialNeedsByStudentIdAsync(studentId);
                if (specialNeeds == null || !specialNeeds.Any())
                    return NotFound("No special needs found for this student.");
                return Ok(specialNeeds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetSpecialNeedByCategory")]
        public async Task<IActionResult> GetSpecialNeedByCategory([FromQuery] int categoryId)
        {
            if (categoryId <= 0)
                return BadRequest("Invalid category ID.");
            try
            {
                var specialNeeds = await _studentSpecialNeedService.GetStudentSpecialNeedsByCategoryIdAsync(categoryId);
                if (specialNeeds == null || !specialNeeds.Any())
                    return NotFound("No special needs found for this category.");
                return Ok(specialNeeds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete]
        [Route("DeleteSpecialNeed")]
        public IActionResult DeleteSpecialNeed([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid special need ID.");
            try
            {
                _studentSpecialNeedService.DeleteStudentSpecialNeed(id);
                return Ok("Special need deleted successfully.");
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting special need: {ex.Message}");
            }

        }
    }
}
