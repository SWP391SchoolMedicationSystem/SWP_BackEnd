using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllBlogsAsync();
        Task<Blog> GetBlogByIdAsync(int id);
        Task AddBlogAsync(BlogDTO dto);
        void UpdateBlog(BlogDTO dto, int id);
        void DeleteBlog(int id);
        Task<List<Blog>> SearchBlogsAsync(string searchTerm);
    }
}
