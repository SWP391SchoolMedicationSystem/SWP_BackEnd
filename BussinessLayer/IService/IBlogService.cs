using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.Blogs;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllBlogsAsync();
        Task<Blog> GetBlogByIdAsync(int id);
        Task AddBlogAsync(CreateBlogDTO dto);
        void UpdateBlog(UpdateBlogDTO dto);
        void DeleteBlog(int id);
        Task<List<Blog>> SearchBlogsAsync(string searchTerm);
        void ApproveBlog(ApproveBlogDTO dto);
        void RejectBlog(RejectBlogDTO dto);
        Task<List<Blog>> GetPublishedBlogs();
        public Task UploadBlogImageAsync(BlogImageUploadDTO dto);
    }
}
