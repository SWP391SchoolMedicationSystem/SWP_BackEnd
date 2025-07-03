using BussinessLayer.IService;
using DataAccessLayer.DTO.SpecialNeedCategory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialNeedCategoryController : ControllerBase
    {
        private readonly ISpecialNeedCategoryService _specialNeedCategoryService;
        public SpecialNeedCategoryController(ISpecialNeedCategoryService specialNeedCategoryService)
        {
            _specialNeedCategoryService = specialNeedCategoryService;
        }
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _specialNeedCategoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid category ID.");
            try
            {
                var category = await _specialNeedCategoryService.GetCategoryByIdAsync(id);
                if (category == null)
                    return NotFound("Category not found.");
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("AddCategory")]
        public IActionResult AddCategory([FromBody] CreateSpecialNeedCategoryDTO categoryDto)
        {
            if (categoryDto == null)
                return BadRequest("Category data is null.");
            try
            {
                _specialNeedCategoryService.AddCategoryAsync(categoryDto);
                return Ok("Category added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding category: {ex.Message}");
            }
        }
        [HttpPut("UpdateCategory")]
        public IActionResult UpdateCategory([FromBody] UpdateSpecialNeedCategoryDTO categoryDto)
        {
            if (categoryDto == null)
                return BadRequest("Category data is null.");
            try
            {
                _specialNeedCategoryService.UpdateCategoryAsync(categoryDto);
                return Ok("Category updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating category: {ex.Message}");
            }
        }
        [HttpDelete("DeleteCategory")]
        public IActionResult DeleteCategory([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid category ID.");
            try
            {
                _specialNeedCategoryService.DeleteCategoryAsync(id);
                return Ok("Category deleted successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Category not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting category: {ex.Message}");
            }
        }
    }
}
