using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;
        private readonly IMapper _mapper;
        public FormController(IFormService formService, IMapper mapper)
        {
            _formService = formService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForms()
        {
            try
            {
                var forms = await _formService.GetAllFormsAsync();
                return Ok(forms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving forms: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormById(int id)
        {
            try
            {
                var form = await _formService.GetFormByIdAsync(id);
                if (form == null)
                {
                    return NotFound($"Form with ID {id} not found.");
                }
                return Ok(form);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving form: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateForm(IFormFile file, [FromBody] CreateFormDTO formDto)
        {
            if (formDto == null)
            {
                return BadRequest("Form data is null.");
            }
            try
            {
                var createdBy = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
                var createdForm = await _formService.CreateFormAsync(formDto, createdBy);
                return CreatedAtAction(nameof(GetFormById), new { id = createdForm.FormId }, createdForm);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating form: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateForm([FromBody] UpdateFormDTO formDto)
        {
            if (formDto == null)
            {
                return BadRequest("Form data is invalid.");
            }
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
                var updatedForm = await _formService.UpdateFormAsync(formDto, updateBy);
                return Ok(updatedForm);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Form with ID {formDto.FormId} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating form: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForm(int id)
        {
            try
            {
                var deleteBy = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
                var result = await _formService.DeleteFormAsync(id, deleteBy);
                if (result)
                {
                    return Ok("Delete Success!");
                }
                return NotFound($"Form with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting form: {ex.Message}");
            }
        }

        [HttpGet("parent/{parentId}")]
        public async Task<ActionResult<List<FormDTO>>> GetFormsByParentId(int parentId)
        {
            try
            {
                var forms = await _formService.GetFormsByParentIdAsync(parentId);
                return Ok(forms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving forms by parent ID: {ex.Message}");
            }
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<List<FormDTO>>> GetFormsByCategoryId(int categoryId)
        {
            try
            {
                var forms = await _formService.GetFormsByCategoryIdAsync(categoryId);
                return Ok(forms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving forms by category ID: {ex.Message}");
            }
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptForm([FromBody] ResponseFormDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Response data is null.");
            }
            try
            {
                var acceptBy = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
                var result = await _formService.AcceptFormAsync(dto, acceptBy);
                if (result)
                {
                    return Ok("Form accepted successfully.");
                }
                return BadRequest("Failed to accept form.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error accepting form: {ex.Message}");
            }
        }

        [HttpPost("decline")]
        public async Task<IActionResult> DeclineForm([FromBody] ResponseFormDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Response data is null.");
            }
            try
            {
                var declineBy = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
                var result = await _formService.DeclineFormAsync(dto, declineBy);
                if (result)
                {
                    return Ok("Form declined successfully.");
                }
                return BadRequest("Failed to decline form.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error declining form: {ex.Message}");
            }
        }
    }
}
