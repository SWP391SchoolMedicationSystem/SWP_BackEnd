﻿using BussinessLayer.IService;
using BussinessLayer.Service;
using DataAccessLayer.DTO.Blogs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HPSF;
using static BussinessLayer.Service.NotificationService;

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
        [Route("getById")]
        public async Task<IActionResult> GetBlogById([FromQuery] int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null)
                return NotFound("Blog not found.");
            return Ok(blog);
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddBlog([FromForm] CreateBlogDTO blogDto)
        {
            if (blogDto == null)
                return BadRequest("Blog data is null.");

            try
            {
                var imageUrl = await _blogService.AddBlogAsync(blogDto);
                return Ok(new { message = "Blog added successfully.", imageUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding blog: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateBlog([FromForm] UpdateBlogDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");
            try
            {
                var imageUrl = await _blogService.UpdateBlog(dto);
                return Ok(new { message = "Blog update successfully.", imageUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating blog: {ex.Message}");
            }
        }
        [HttpDelete]
        [Route("delete")]
        public void DeleteBlog([FromQuery] int id)
        {
            if (id < 0)
                throw new ArgumentException("Invalid blog ID.");
            try
            {
                _blogService.DeleteBlog(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting blog: {ex.Message}", ex);
            }
        }
        [HttpGet]
        [Route("GetPublishedBlogs")]
        public async Task<IActionResult> GetPublishedBlogs()
        {
            var blogs = await _blogService.GetPublishedBlogs();
            return Ok(blogs);
        }
        [HttpPost]
        [Route("ApproveBlog")]
        [ProducesResponseType<int>(StatusCodes.Status200OK)]
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
        [HttpPost]
        [Route("RejectBlog")]
        [ProducesResponseType<int>(StatusCodes.Status200OK)]
        public IActionResult RejectBlog([FromBody] RejectBlogDTO rejectBlogDto)
        {
            if (rejectBlogDto == null)
                return BadRequest("Invalid rejection data.");
            if (rejectBlogDto.BlogId < 0)
                return BadRequest("Invalid blog ID.");
            try
            {
                _blogService.RejectBlog(rejectBlogDto);
                return Ok(new { Message = rejectBlogDto.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error rejecting blog: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("SearchBlogs")]
        public async Task<IActionResult> SearchBlogs(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return BadRequest("Search term cannot be empty.");
            try
            {
                var blogs = await _blogService.SearchBlogsAsync(searchTerm);
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error searching blogs: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetRejectedBlogs")]
        public async Task<IActionResult> GetRejectedBlogs()
        {
            try
            {
                var blogs = await _blogService.GetRejectedBlogs();
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving rejected blogs: {ex.Message}");
            }
        }
    }
}
