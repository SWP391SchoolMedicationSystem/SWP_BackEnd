using BussinessLayer.IService;
using BussinessLayer.Service;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await _blogService.GetAllBlogsAsync();
            return Ok(blogs);
        }
        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null)
                return NotFound("Blog not found.");
            return Ok(blog);
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddBlog([FromBody] BlogDTO blogDto)
        {
            if (blogDto == null)
                return BadRequest("Blog data is null.");
            await _blogService.AddBlogAsync(blogDto);
            return Ok("Blog added successfully.");
        }
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult UpdateBlog([FromBody] BlogDTO dto, int id)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            _blogService.UpdateBlog(dto, id);
            return Ok("Health record updated.");
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public void DeleteBlog(int id)
        {
            var blog = _blogService.GetBlogByIdAsync(id).Result;
            if (blog == null)
            {
                throw new KeyNotFoundException($"Blog with id {id} not found.");
            }
            _blogService.DeleteBlog(id);
        }
    }
}
