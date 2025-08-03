using BussinessLayer.IService;
using DataAccessLayer.DTO.MedicineCategory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineCategoryController : ControllerBase
    {
        private readonly IMedicineCategoryService _medicineCategoryService;
        public MedicineCategoryController(IMedicineCategoryService medicineCategoryService)
        {
            _medicineCategoryService = medicineCategoryService;
        }
        [HttpGet]
        [Route("GetAllMedicineCategories")]
        public async Task<IActionResult> GetAllMedicineCategories()
        {
            try
            {
                var categories = await _medicineCategoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid category ID.");
            try
            {
                var category = await _medicineCategoryService.GetCategoryByIdAsync(id);
                if (category == null)
                    return NotFound("Category not found.");
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] CreateMedicineCategoryDTO categoryDto)
        {
            if (categoryDto == null)
                return BadRequest("Category data is null.");
            try
            {
                await _medicineCategoryService.AddCategory(categoryDto);
                if (string.IsNullOrWhiteSpace(categoryDto.Medicinecategoryname) || string.IsNullOrWhiteSpace(categoryDto.Description))
                    return BadRequest("Category name or description cannot be empty.");
                return Ok("Category added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding category: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateMedicineCategoryDTO categoryDto)
        {
            if (categoryDto == null || categoryDto.Medicinecategoryid <= 0)
                return BadRequest("Invalid category data.");
            try
            {
                await _medicineCategoryService.UpdateCategory(categoryDto);
                if (string.IsNullOrWhiteSpace(categoryDto.Medicinecategoryname) || string.IsNullOrWhiteSpace(categoryDto.Description))
                    return BadRequest("Category name or description cannot be empty.");
                return Ok("Category updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating category: {ex.Message}");
            }
        }
    }
}
