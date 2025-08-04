using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO.Staffs;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;
        private readonly IMapper _mapper;
        private static readonly string[] validationSettings = ["251792493601-lkt15jmuh1jfr1cvgd0a45uamdqusosg.apps.googleusercontent.com"];

        public StaffController(IStaffService staffService, IMapper mapper)
        {
            _staffService = staffService;
            _mapper = mapper;
        }
        [HttpPost("registration")]
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
        [HttpPost("staff/{id}")]
        public async Task<IActionResult> GetStaffById(int id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if (staff == null)
            {
                return NotFound($"Staff with id {id} not found.");
            }
            return Ok(staff);
        }

        [HttpGet("staff")]
        public async Task<IActionResult> GetAllStaff()
        {
            var staffList = await _staffService.GetAllStaffAsync();
            return Ok(staffList);
        }

        [HttpGet("nurses")]
        public async Task<IActionResult> GetAllNurses()
        {
            var nurses = await _staffService.GetAllNurseAsync();
            return Ok(nurses);
        }

        [HttpDelete("staff/{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            try
            {
                await _staffService.DeleteStaff(id);
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
        [HttpPut("staff")]
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

    }
}
