using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO.HealthStatus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthStatusCategoryController : ControllerBase
    {
        private readonly IHealthStatusCategoryService _healthStatusCategoryService;
        private readonly IMapper _mapper;
        public HealthStatusCategoryController(IHealthStatusCategoryService healthStatusCategoryService, IMapper mapper)
        {
            _healthStatusCategoryService = healthStatusCategoryService;
            _mapper = mapper;
        }
        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _healthStatusCategoryService.GetHealthstatuscategories();
            var categoriesDto = _mapper.Map<List<HealthStatusCategoryDTO>>(categories);
            return Ok(categoriesDto);
        }
        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetCategoryById([FromBody] int id)
        {
            var category = await _healthStatusCategoryService.GetHealthstatuscategoryByID(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryDto = _mapper.Map<HealthStatusCategoryDTO>(category);
            return Ok(categoryDto);
        }
        [HttpPost("category")]
        public async Task<IActionResult> AddCategory([FromBody] HealthStatusCategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Invalid category data.");
            }
            await _healthStatusCategoryService.AddHealthStatusCategory(categoryDto);
            return CreatedAtAction(nameof(GetAllCategories), new { id = categoryDto.HealthStatusCategoryName }, categoryDto);
        }
        [HttpPut("category")]
        public IActionResult UpdateCategory([FromBody] HealthStatusCategoryDTO categoryDto)
        {

            _healthStatusCategoryService.UpdateHealthstatuscategory(categoryDto);
            return NoContent();
        }
        [HttpDelete("category/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _healthStatusCategoryService.DeleteHealthstatuscategory(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
