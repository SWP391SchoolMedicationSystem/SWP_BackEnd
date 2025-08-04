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
        Task <String> AddBlogAsync(CreateBlogDTO dto);
        Task <String> UpdateBlog(UpdateBlogDTO dto);
        Task DeleteBlog(int id);
        Task<List<BlogDTO>> SearchBlogsAsync(string searchTerm);
        Task ApproveBlog(ApproveBlogDTO dto);
        Task RejectBlog(RejectBlogDTO dto);
        Task<List<BlogDTO>> GetPublishedBlogs();
        Task<List<BlogDTO>> GetRejectedBlogs();
        //        public Task<string> UploadBlogImageAsync(BlogImageUploadDTO dto);   
    }
}
