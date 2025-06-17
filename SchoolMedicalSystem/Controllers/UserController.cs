using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Service;
using BussinessLayer.Utils;
using DataAccessLayer.DTO;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService, IMapper mapper) : ControllerBase
    {
        [HttpGet("user")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userService.GetAllAsync();
            var usersDto = mapper.Map<List<UserDTo>>(users);
            return Ok(users);
        }
    
    [HttpPost("user")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (login == null)
            {
                return BadRequest("Login data is null.");
            }
            try
            {
                string token = await userService.Login(login);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("google")]
        public async Task<IActionResult> VerifyGoogleToken([FromBody] TokenRequest request)
        {
            try
            {
                var payload = await userService.ValidateGoogleToken(request.Credential);
                return Ok(payload);
            }
            catch (InvalidJwtException)
            {
                return Unauthorized("Token không hợp lệ.");
            }
        }

    }

}
