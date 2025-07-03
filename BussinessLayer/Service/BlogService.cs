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
        private readonly IStaffRepository _staffRepository;
        private readonly AppSetting _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //        private readonly IStaffRepository _staffRepository;
        public BlogService(
            IBlogRepo blogRepo,
            IMapper mapper,
            IOptionsMonitor<AppSetting> option,
            IStaffRepository staffRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _blogRepo = blogRepo;
            _staffRepository = staffRepository;
            _mapper = mapper;
            _appSettings = option.CurrentValue;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<BlogDTO>> GetAllBlogsAsync()
        {
            List<Blog> blogs = await _blogRepo.GetAllAsync();
            List<BlogDTO> blogDTOs = _mapper.Map<List<BlogDTO>>(blogs);
            foreach (var blog in blogDTOs)
            {
                // Set the image URL to be absolute
                blog.UpdatedByName = blog.UpdatedBy != null ? _staffRepository.GetByIdAsync(blog.UpdatedBy.Value).Result?.Fullname : "Chưa Update";
                blog.CreatedByName = blog.CreatedBy != null ? _staffRepository.GetByIdAsync(blog.CreatedBy.Value).Result?.Fullname : "Unknown";
                blog.ApprovedByName = blog.ApprovedBy != null ? _staffRepository.GetByIdAsync(blog.ApprovedBy.Value).Result?.Fullname : "Chưa được approved";
            }
            return blogDTOs;
        }
        public async Task<BlogDTO> GetBlogByIdAsync(int id)
        {
            var blog = await _blogRepo.GetByIdAsync(id);
            BlogDTO blogDTOs = _mapper.Map<BlogDTO>(blog);

            blogDTOs.UpdatedByName = blogDTOs.UpdatedBy != null ? _staffRepository.GetByIdAsync(blog.UpdatedBy.Value).Result?.Fullname : "Chưa Update";
            blogDTOs.CreatedByName = blogDTOs.CreatedBy != null ? _staffRepository.GetByIdAsync(blog.CreatedBy).Result?.Fullname : "Unknown";
            blogDTOs.ApprovedByName = blogDTOs.ApprovedBy != null ? _staffRepository.GetByIdAsync(blog.ApprovedBy.Value).Result?.Fullname : "Chưa được approved";
            return blogDTOs;

        }
        public async Task AddBlogAsync(CreateBlogDTO dto)
        {

                Blog blog = _mapper.Map<Blog>(dto);
                blog.Status = "Draft";
                blog.CreatedAt = DateTime.Now;
                await _blogRepo.AddAsync(blog);
                _blogRepo.Save();

        }
        public async Task UpdateBlog(UpdateBlogDTO dto)
        {
            var entity = await _blogRepo.GetByIdAsync(dto.BlogID);
            if (dto != null)
            {
                try
                {
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
                        //                    entity.Image = dto.Image;

                        _blogRepo.Update(entity);
                        _blogRepo.Save();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error updating blog. Please try again later.");
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
        public Task<List<BlogDTO>> SearchBlogsAsync(string searchTerm)
        {
            var blogs = _blogRepo.GetAllAsync().Result.Where(b => b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            List<BlogDTO> blogDTOs = _mapper.Map<List<BlogDTO>>(blogs);
            foreach (var blog in blogDTOs)
            {
                // Set the image URL to be absolute
                blog.UpdatedByName = blog.UpdatedBy != null ? _staffRepository.GetByIdAsync(blog.UpdatedBy.Value).Result?.Fullname : "Chưa Update";
                blog.CreatedByName = blog.CreatedBy != null ? _staffRepository.GetByIdAsync(blog.CreatedBy.Value).Result?.Fullname : "Unknown";
                blog.ApprovedByName = blog.ApprovedBy != null ? _staffRepository.GetByIdAsync(blog.ApprovedBy.Value).Result?.Fullname : "Chưa được approved";
            }
            return Task.FromResult(blogDTOs);

        }
        public void ApproveBlog(ApproveBlogDTO dto)
        {
            var blog = _blogRepo.GetByIdAsync(dto.BlogId).Result;
            if (blog != null)
            {
                blog.ApprovedBy = dto.ApprovedBy;
                blog.ApprovedOn = DateTime.Now;
                blog.Status = "Published";
                _blogRepo.Update(blog);
                _blogRepo.Save();
            }
        }
        public Task<List<BlogDTO>> GetPublishedBlogs()
        {
            var blogs = _blogRepo.GetAllAsync().Result.Where(b =>
                    b.IsDeleted != true &&
//                    b.ApprovedBy != null &&
 //                   b.ApprovedOn != null &&
                    b.Status != null &&
                    (b.Status.Equals("Published", StringComparison.OrdinalIgnoreCase)))
                .ToList();
            List<BlogDTO> blogDTOs = _mapper.Map<List<BlogDTO>>(blogs);
            foreach (var blog in blogDTOs)
            {
                // Set the image URL to be absolute
                blog.UpdatedByName = blog.UpdatedBy != null ? _staffRepository.GetByIdAsync(blog.UpdatedBy.Value).Result?.Fullname : "Chưa Update";
                blog.CreatedByName = blog.CreatedBy != null ? _staffRepository.GetByIdAsync(blog.CreatedBy.Value).Result?.Fullname : "Unknown";
                blog.ApprovedByName = blog.ApprovedBy != null ? _staffRepository.GetByIdAsync(blog.ApprovedBy.Value).Result?.Fullname : "Chưa được approved";
            }
            return Task.FromResult(blogDTOs);


        }
        public void RejectBlog(RejectBlogDTO dto)
        {
            var blog = _blogRepo.GetByIdAsync(dto.BlogId).Result;
            if (blog != null)
            {
                blog.ApprovedBy = dto.ApprovedBy;
                blog.ApprovedOn = DateTime.Now;
                blog.Status = "Rejected";
                _blogRepo.Update(blog);
                _blogRepo.Save();
            }
        }
        public async Task<string> UploadBlogImageAsync(BlogImageUploadDTO dto)
        {
            var blog = await _blogRepo.GetByIdAsync(dto.BlogId);
            if (blog == null) throw new Exception("Blog not found.");

            //only jpg, jpeg, png file allow
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(dto.ImageFile.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
                throw new Exception("Only JPG and PNG files are allowed.");

            //size < 2mb
            if (dto.ImageFile.Length > 2 * 1024 * 1024)
                throw new Exception("File size must be less than 2MB.");

            // Save image to wwwroot/images/blogs
            var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "blogs");
            Directory.CreateDirectory(wwwRootPath); //create directory if it doesn't exist

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName); //generate unique name for image file
            var filePath = Path.Combine(wwwRootPath, fileName); //get full path for the image file

            // open file stream and copy the uploaded file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.ImageFile.CopyToAsync(stream); //copy the uploaded file to the server
            }

            // Update blog entity
            blog.Image = $"/images/blogs/{fileName}";
            blog.UpdatedAt = DateTime.Now;
            _blogRepo.Update(blog);
            _blogRepo.Save();

            return blog.Image; //return the image URL
        }
    }            
}
