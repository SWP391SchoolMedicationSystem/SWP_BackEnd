using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Utils;
using DataAccessLayer.DTO;
using DataAccessLayer.IRepository;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _parentservice;
        private readonly IMapper _mapper;
        public ParentController(IParentService parentservice, IMapper mapper)
        {
            _parentservice = parentservice;
            _mapper = mapper;
        }
        [HttpGet("GetAllParents")]
        public async Task<IActionResult> GetAllParents()
        {
            var parents = await _parentservice.GetAllParentsAsync();
            return Ok(parents);
        }
        [HttpGet("GetParentById/{id}")]
        public async Task<IActionResult> GetParentById(int id)
        {
            var parent = await _parentservice.GetParentByIdAsync(id);
            if (parent == null)
            {
                return NotFound($"Parent with id {id} not found.");
            }
            return Ok(parent);
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] ParentRegister parent)
        {
            try
            {
                if (parent == null)
                {
                    return BadRequest("Parent data is null.");
                }
                await _parentservice.AddParentAsync(parent);
                return CreatedAtAction(nameof(GetParentById), new { id = parent.Fullname }, parent);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                if (login == null)
                {
                    return BadRequest("Login data is null.");
                }
                var token = await _parentservice.GenerateToken(login);
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
        [HttpPut("UpdateParent")]
        public IActionResult UpdateParentRequest([FromBody] ParentUpdate parent)
        {
            try
            {
                if (parent == null)
                {
                    return BadRequest("Parent data is null.");
                }
                _parentservice.UpdateParent(parent);
                return Ok("Parent request updated successfully.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("google")]
        public async Task<IActionResult> VerifyGoogleToken([FromBody] TokenRequest request)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(request.Credential, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { "439095486459-gvdm000c5lstr8v0j1cl3ng9bg4gs4l2.apps.googleusercontent.com" } // Thay bằng client ID của bạn
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
