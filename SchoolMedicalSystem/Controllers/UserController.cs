using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Service;
using BussinessLayer.Utils;
using DataAccessLayer.DTO;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
            var usersDto = mapper.Map<List<UserDTO>>(users);
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

        //Step 1 (User enter email to reset password)
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email cannot be empty or null");
            var success = await userService.SendOTPEmailAsync(email);
            if (!success)
                return NotFound("Email not found!");

            return Ok("Reset email have been sent!");
        }

        //Step 2 (User enter OTP in the mail that been sent to their email in Step 1)
        [HttpPost("ValidateOTP")]
        public async Task<IActionResult> ValidateOTP([FromBody] OtpDTO otp)
        {
            if (string.IsNullOrEmpty(otp.OtpCode))
                return BadRequest("OTP cannot be empty or null");
            var isValid = await userService.ValidateOtpAsync(new OtpDTO { OtpCode = otp.OtpCode, Email = otp.Email });
            if (!isValid)
                return BadRequest("Invalid OTP");
            return Ok("OTP is valid");
        }

        // Step 3 (User enter new password to reset password)
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.NewPassword))
                return BadRequest("Email and new password cannot be empty or null");
            var success = await userService.ResetPassword(request.Email, request.NewPassword);
            if (!success)
                return NotFound("Failed to reset password");
            return Ok("Password reset successfully");
        }

        [HttpPost("google")]
        public async Task<IActionResult> VerifyGoogleToken([FromBody] TokenRequest request)
        {
            try
            {
                var payload = await userService.ValidateGoogleToken(request.Credential);
                if (payload != null)
                {
                    return Ok(new
                    {
                        Token = payload,
                    });
                }
                return Ok("Can't find");
            }
            catch (InvalidJwtException)
            {
                return Unauthorized("Token không hợp lệ.");
            }
        }

    }
    public class ResetPasswordDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }

}
