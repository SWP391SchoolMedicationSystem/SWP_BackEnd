using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Utils;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Parents;
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
        private static readonly string[] validationSettings = ["251792493601-lkt15jmuh1jfr1cvgd0a45uamdqusosg.apps.googleusercontent.com"];

        public ParentController(IParentService parentservice, IMapper mapper)
        {
            _parentservice = parentservice;
            _mapper = mapper;
        }
        [HttpGet("parent")]
        public async Task<IActionResult> GetAllParents()
        {
            var parents = await _parentservice.GetAllParentsAsync();
            return Ok(parents);
        }
        [HttpGet("parent/{id}")]
        public async Task<IActionResult> GetParentById(int id)
        {
            var parent = await _parentservice.GetParentByIdAsync(id);
            if (parent == null)
            {
                return NotFound($"Parent with id {id} not found.");
            }
            return Ok(parent);
        }
        [HttpPost("registration")]
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

        [HttpPut("parent")]
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
        [HttpDelete("parent/{id}")]
        public IActionResult DeleteParent(int id)
        {
            try
            {
                _parentservice.DeleteParent(id);
                return Ok("Parent deleted successfully.");
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
