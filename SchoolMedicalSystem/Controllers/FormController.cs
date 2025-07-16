using AutoMapper;
using Azure.Core;
using BussinessLayer.IService;
using BussinessLayer.Utils;
using DataAccessLayer.DTO.Form;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly FileHandler _fileHandler;
        private readonly IFormService _formService;
        private readonly IMapper _mapper;
        public FormController(IFormService formService, IMapper mapper, FileHandler fileHandler)
        {
            _formService = formService;
            _mapper = mapper;
            _fileHandler = fileHandler;
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
                var result = await _formService.AcceptFormAsync(dto);
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
                var result = await _formService.DeclineFormAsync(dto);
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
        [HttpPost]
        [Route("form/medicinerequest")]
        [Consumes("multipart/form-data"), DisableRequestSizeLimit]
        public async Task<ActionResult<Form>> CreateFormForMedicineRequest([FromForm] AddFormMedicine form)
        {
            string? storedFileName = null;
            string? accessToken = null;

            if (form.DocumentFile != null)
            {
                var uploadResult = await _fileHandler.UploadAsync(form.DocumentFile);
                if (!uploadResult.Success)
                {
                    return null;//BadRequest(uploadResult.ErrorMessage);
                }
                storedFileName = uploadResult.StoredFileName;
                accessToken = Guid.NewGuid().ToString();
            }


            var createdForm = await _formService.AddFormMedicineRequest(form, storedFileName, accessToken);
            if (createdForm == null)
            {
                return BadRequest("Failed to create form for medicine request.");
            }
            return CreatedAtAction(nameof(GetFormById), new { id = createdForm.FormId }, createdForm);
        }
        [HttpPost]
        [Route("form/absentrequest")]
        [Consumes("multipart/form-data"), DisableRequestSizeLimit]
        public async Task<ActionResult<Form>> CreateFormForAbsentRequest([FromForm] AddFormAbsent form)
        {
            string? storedFileName = null;
            string? accessToken = null;

            if (form.DocumentFile != null)
            {
                var uploadResult = await _fileHandler.UploadAsync(form.DocumentFile);
                if (!uploadResult.Success)
                {
                    return null;//BadRequest(uploadResult.ErrorMessage);
                }
                storedFileName = uploadResult.StoredFileName;
                accessToken = Guid.NewGuid().ToString();
            }


            var createdForm = await _formService.AddFormMedicineRequest(form, storedFileName, accessToken);
            if (createdForm == null)
            {
                return BadRequest("Failed to create form for medicine request.");
            }
            return CreatedAtAction(nameof(GetFormById), new { id = createdForm.FormId }, createdForm);
        }
        [HttpPost]
        [Route("form/permissionrequest")]
        [Consumes("multipart/form-data"), DisableRequestSizeLimit]
        public
            async Task<ActionResult<Form>> CreateFormForChronicIllness([FromForm] AddFormChronicIllness form)
        {
            string? storedFileName = null;
            string? accessToken = null;
            if (form.DocumentFile != null)
            {
                var uploadResult = await _fileHandler.UploadAsync(form.DocumentFile);
                if (!uploadResult.Success)
                {
                    return null;//BadRequest(uploadResult.ErrorMessage);
                }
                storedFileName = uploadResult.StoredFileName;
                accessToken = Guid.NewGuid().ToString();
            }
            var createdForm = await _formService.AddFormMedicineRequest(form, storedFileName, accessToken);
            if (createdForm == null)
            {
                return BadRequest("Failed to create form for chronic illness request.");
            }
            return CreatedAtAction(nameof(GetFormById), new { id = createdForm.FormId }, createdForm);
        }
        [HttpPost]
        [Route("form/physicalrequest")]
        [Consumes("multipart/form-data"), DisableRequestSizeLimit]
        public async Task<ActionResult<Form>> CreateFormForPhysicalModificiationRequest([FromForm] AddFormPhysicalActivityModification form)
        {
            string? storedFileName = null;
            string? accessToken = null;
            if (form.DocumentFile != null)
            {
                var uploadResult = await _fileHandler.UploadAsync(form.DocumentFile);
                if (!uploadResult.Success)
                {
                    return null;//BadRequest(uploadResult.ErrorMessage);
                }
                storedFileName = uploadResult.StoredFileName;
                accessToken = Guid.NewGuid().ToString();
            }
            var createdForm = await _formService.AddFormMedicineRequest(form, storedFileName, accessToken);
            if (createdForm == null)
            {
                return BadRequest("Failed to create form for chronic illness request.");
            }
            return CreatedAtAction(nameof(GetFormById), new { id = createdForm.FormId }, createdForm);
        }
        [HttpPost]
        [Route("form/otherrequest")]
        [Consumes("multipart/form-data"), DisableRequestSizeLimit]
        public async Task<ActionResult<Form>> CreateFormForOtherRequest([FromForm] CreateFormDTO form)
        {
            string? storedFileName = null;
            string? accessToken = null;
            if (form.DocumentFile != null)
            {
                var uploadResult = await _fileHandler.UploadAsync(form.DocumentFile);
                if (!uploadResult.Success)
                {
                    return null;//BadRequest(uploadResult.ErrorMessage);
                }
                storedFileName = uploadResult.StoredFileName;
                accessToken = Guid.NewGuid().ToString();
            }
            var createdForm = await _formService.AddFormMedicineRequest(form, storedFileName, accessToken);
            if (createdForm == null)
            {
                return BadRequest("Failed to create form for chronic illness request.");
            }
            return CreatedAtAction(nameof(GetFormById), new { id = createdForm.FormId }, createdForm);
        }






    }
}
