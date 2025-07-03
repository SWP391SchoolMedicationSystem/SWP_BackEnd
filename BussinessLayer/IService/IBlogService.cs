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
        Task<List<BlogDTO>> GetAllBlogsAsync();
        Task<BlogDTO> GetBlogByIdAsync(int id);
        Task AddBlogAsync(CreateBlogDTO dto);
        Task UpdateBlog(UpdateBlogDTO dto);
        void DeleteBlog(int id);
        Task<List<BlogDTO>> SearchBlogsAsync(string searchTerm);
        void ApproveBlog(ApproveBlogDTO dto);
        void RejectBlog(RejectBlogDTO dto);
        Task<List<BlogDTO>> GetPublishedBlogs();
        public Task<string> UploadBlogImageAsync(BlogImageUploadDTO dto);
    }
}
