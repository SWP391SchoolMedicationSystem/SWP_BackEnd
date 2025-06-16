using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Utils;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Staffs;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;
        private readonly IMapper _mapper;
        private static readonly string[] validationSettings = ["439095486459-gvdm000c5lstr8v0j1cl3ng9bg4gs4l2.apps.googleusercontent.com"];

        public StaffController(IStaffService staffService, IMapper mapper)
        {
            _staffService = staffService;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] StaffRegister register)
        {
            try
            {
                if (register == null)
                {
                    return BadRequest("Staff registration data is null.");
                }

                await _staffService.AddStaffAsync(register);
                return CreatedAtAction(nameof(GetAllStaff), new { email = register.Email }, register);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("GetStaffById/{id}")]
        public async Task<IActionResult> GetStaffById(int id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if (staff == null)
            {
                return NotFound($"Staff with id {id} not found.");
            }
            return Ok(staff);
        }
        [HttpGet("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                if (login == null)
                {
                    return BadRequest("Login data is null.");
                }
                var token = await _staffService.GenerateToken(login);
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized("Invalid credentials.");
                }
                return Ok(new { Token = token });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("GetAllStaff")]
        public async Task<IActionResult> GetAllStaff()
        {
            var staffList = await _staffService.GetAllStaffAsync();
            return Ok(staffList);
        }
        [HttpDelete("DeleteStaff/{id}")]
        public IActionResult DeleteStaff(int id)
        {
            try
            {
                _staffService.DeleteStaff(id);
                return Ok($"Staff with ID {id} deleted successfully.");
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while deleting staff: {ex.Message}");
            }
        }
        [HttpPut("UpdateStaff")]
        public IActionResult UpdateStaff([FromBody] StaffUpdate staffUpdate)
        {
            try
            {
                if (staffUpdate == null)
                {
                    return BadRequest("Staff update data is null.");
                }
                _staffService.UpdateStaff(staffUpdate);
                return Ok("Staff updated successfully.");
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while updating staff: {ex.Message}");
            }
        }
        [HttpPost("google")]
        public async Task<IActionResult> VerifyGoogleToken([FromBody] TokenRequest request)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(request.Credential, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = validationSettings
                });

                return Ok(new
                {
                    payload.Email,
                    payload.Name,
                    payload.Picture,
                    payload.Subject // ID người dùng Google
                });
            }
            catch (InvalidJwtException)
            {
                return Unauthorized("Token không hợp lệ.");
            }
        }
    }
}
