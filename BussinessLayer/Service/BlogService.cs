using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Utils.Configurations;
using DataAccessLayer.DTO.Blogs;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BussinessLayer.Service
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepo _blogRepo;
        private readonly IMapper _mapper;
        private readonly AppSetting _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //        private readonly IStaffRepository _staffRepository;
        public BlogService(
            IBlogRepo blogRepo,
            IMapper mapper,
            IOptionsMonitor<AppSetting> option,
            IHttpContextAccessor httpContextAccessor)
        {
            _blogRepo = blogRepo;
            _mapper = mapper;
            _appSettings = option.CurrentValue;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Blog>> GetAllBlogsAsync()
        {
            List<Blog> blogs = _mapper.Map<List<Blog>>(await _blogRepo.GetAllAsync());
            return blogs;
        }
        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            return await _blogRepo.GetByIdAsync(id);
        }
        public async Task AddBlogAsync(CreateBlogDTO dto)
        {
            if (dto != null)
            {
                Blog blog = _mapper.Map<Blog>(dto);
                await _blogRepo.AddAsync(blog);
                _blogRepo.Save();
            }
        }
        public void UpdateBlog(UpdateBlogDTO dto, int id)
        {
            if (dto != null)
            {
                var entity = _blogRepo.GetByIdAsync(id).Result;
                if (entity != null)
                {
                    entity.Title = dto.Title;
                    entity.Content = dto.Content;
                    //                    entity.ApprovedBy = dto.ApprovedBy;
                    //                    entity.ApprovedOn = dto.ApprovedOn;
                    //                    entity.CreatedBy = dto.CreatedBy;
                    //                   entity.CreatedAt = DateTime.Now;
                    entity.UpdatedAt = DateTime.Now;
                    entity.UpdatedBy = dto.UpdatedBy;
                    entity.Status = dto.Status;
                    entity.IsDeleted = dto.IsDeleted;
                    entity.Image = dto.Image;

                    _blogRepo.Update(entity);
                    _blogRepo.Save();
                }
            }
        }
        public void DeleteBlog(int id)
        {
            var entity = _blogRepo.GetByIdAsync(id).Result;
            if (entity != null)
            {
                entity.IsDeleted = true;
                _blogRepo.Update(entity);
                _blogRepo.Save();
            }
        }
        public Task<List<Blog>> SearchBlogsAsync(string searchTerm)
        {
            var blogs = _blogRepo.GetAllAsync().Result;
            return Task.FromResult(blogs.Where(b => b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                                     b.Content.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList());
        }
        public void ApproveBlog(ApproveBlogDTO dto, int id)
        {
            var blog = _blogRepo.GetByIdAsync(id).Result;
            if (blog != null)
            {
                blog.ApprovedBy = dto.ApprovedBy;
                blog.ApprovedOn = DateTime.Now;
                blog.Status = "Approved";
                _blogRepo.Update(blog);
                _blogRepo.Save();
            }
        }
        public async Task<List<Blog>> GetPublishedBlogs()
        {
            var blogs = await _blogRepo.GetAllAsync();
            return blogs
                .Where(b =>
                    b.IsDeleted != true &&
                    b.ApprovedBy != null &&
                    b.ApprovedOn != null &&
                    b.Status != null &&
                    (b.Status.Equals("Published", StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }
    }
}
