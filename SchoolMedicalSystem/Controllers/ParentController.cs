using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO.Parents;
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
        [HttpGet("parent")]
        public async Task<IActionResult> GetAllParents()
        {
            var parents = await _parentservice.GetAllParentsAsync();
            return Ok(parents);
        }
        [HttpGet("parent/{id}")]
        public async Task<IActionResult> GetParentById([FromBody] int id)
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
        public async Task<IActionResult> UpdateParentRequest([FromBody] ParentUpdate parent)
        {
            try
            {
                if (parent == null)
                {
                    return BadRequest("Parent data is null.");
                }
                await _parentservice.UpdateParent(parent);
                return Ok("Parent request updated successfully.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("parent/{id}")]
        public async Task<IActionResult> DeleteParent([FromBody] int id)
        {
            try
            {
                await _parentservice.DeleteParent(id);
                return Ok("Parent deleted successfully.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




    }
}
