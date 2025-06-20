using BussinessLayer.IService;
using BussinessLayer.Service;
using DataAccessLayer.DTO.Blogs;
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
        [Route("blog")]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await _blogService.GetAllBlogsAsync();
            return Ok(blogs);
        }
        [HttpGet]
        [Route("blog/{id}")]
        public async Task<IActionResult> GetBlogById([FromBody] int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null)
                return NotFound("Blog not found.");
            return Ok(blog);
        }
        [HttpPost]
        [Route("blog")]
        public async Task<IActionResult> AddBlog([FromBody] CreateBlogDTO blogDto)
        {
            if (blogDto == null)
                return BadRequest("Blog data is null."); 
            try
            {
                await _blogService.AddBlogAsync(blogDto);
                return Ok("Blog added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding blog: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("blog/{id}")]
        public IActionResult UpdateBlog([FromBody] UpdateBlogDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");
            try
            {
                _blogService.UpdateBlog(dto);
                return Ok("Health record updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating blog: {ex.Message}");
            }
        }
        [HttpDelete]
        [Route("blog/{id}")]
        public void DeleteBlog([FromBody] int id)
        {
            if(id < 0)
                throw new ArgumentException("Invalid blog ID.");
            try
            {
                var blog = _blogService.GetBlogByIdAsync(id).Result;
                _blogService.DeleteBlog(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting blog: {ex.Message}", ex);
            }
        }
        [HttpGet]
        [Route("publishedblog")]
        public async Task<IActionResult> GetPublishedBlogs()
        {
            var blogs = await _blogService.GetPublishedBlogs();
            return Ok(blogs);
        }
        [HttpPost]
        [Route("blogapproval/{id}")]
        public IActionResult ApproveBlog([FromBody] ApproveBlogDTO approveBlogDto)
        {
            if (approveBlogDto == null)
                return BadRequest("Invalid approval data.");
            if (approveBlogDto.BlogId < 0)
                return BadRequest("Invalid blog ID.");
            try
            {
                _blogService.ApproveBlog(approveBlogDto);
                return Ok("Blog approved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error approving blog: {ex.Message}");
            }
        }
    }
}
