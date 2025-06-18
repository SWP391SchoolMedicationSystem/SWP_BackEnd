using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Utils.Configurations;
using DataAccessLayer.DTO;
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
        public async Task AddBlogAsync(BlogDTO dto)
        {
            if (dto != null)
            {
                Blog blog = _mapper.Map<Blog>(dto);
                await _blogRepo.AddAsync(blog);
                _blogRepo.Save();
            }
        }
        public void UpdateBlog(BlogDTO dto, int id)
        {
            if (dto != null)
            {
                var entity = _blogRepo.GetByIdAsync(id).Result;
                if (entity != null)
                {
                    entity.Title = dto.Title;
                    entity.Content = dto.Content;
                    entity.ApprovedBy = dto.ApprovedBy;
                    entity.ApprovedOn = dto.ApprovedOn;
                    entity.CreatedBy = dto.CreatedBy;
                    entity.CreatedAt = DateTime.Now;
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
                _blogRepo.Delete(id);
                _blogRepo.Save();
            }
        }
        public Task<List<Blog>> SearchBlogsAsync(string searchTerm)
        {
            var blogs = _blogRepo.GetAllAsync().Result;
            return Task.FromResult(blogs.Where(b => b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                                     b.Content.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList());
        }

    }
}
