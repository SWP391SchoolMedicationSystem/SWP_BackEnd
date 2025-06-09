using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.IRepository;
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
        public async Task<IActionResult> Login([FromBody] ParentLoginDTO login)
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
    }
}
