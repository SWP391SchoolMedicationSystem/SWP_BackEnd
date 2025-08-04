using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Utils.Configurations;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
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
        private readonly IParentRepository _parentRepository;
        private readonly IUserRepository _userRepository;
        private readonly AppSetting _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Cloudinary _cloudinary;
        //        private readonly IStaffRepository _staffRepository;
        const string draft = "Draft";
        const string published = "Published";
        const string rejected = "Rejected";
        public BlogService(
            IBlogRepo blogRepo,
            IUserRepository userRepository,
            IParentRepository parentRepository,
            IMapper mapper,
            IOptionsMonitor<AppSetting> option,
            IStaffRepository staffRepository,
            IHttpContextAccessor httpContextAccessor,
            Cloudinary cloudinary)
        {
            _userRepository = userRepository;
            _blogRepo = blogRepo;
            _staffRepository = staffRepository;
            _parentRepository = parentRepository;
            _mapper = mapper;
            _appSettings = option.CurrentValue;
            _httpContextAccessor = httpContextAccessor;
            _cloudinary = cloudinary;
        }
        public async Task<List<BlogDTO>> GetAllBlogsAsync()
        {
            List<Blog> blogs = await _blogRepo.GetAllAsync();
            List<BlogDTO> blogDTOs = _mapper.Map<List<BlogDTO>>(blogs);
            foreach (var blog in blogDTOs)
            {
                if (blog.CreatedBy != null)
                {
                    //if (_userRepository.GetByIdAsync(blog.CreatedBy.Value).Result.IsStaff)
                    //    blog.CreatedByName = _staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.CreatedBy)?.Fullname ?? "Unknown";
                    //else
                    //    blog.CreatedByName = _parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.CreatedBy)?.Fullname ?? "Unknown";
                    blog.CreatedByName = _staffRepository.GetAllAsync().Result
                    .FirstOrDefault(s => s.Userid == blog.CreatedBy)?.Fullname ?? "Unknown";
                }

                if (blog.UpdatedBy != null)
                {
                    //if (_userRepository.GetByIdAsync(blog.UpdatedBy.Value).Result.IsStaff)
                    //    blog.UpdatedByName = _staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.UpdatedBy)?.Fullname ?? "Unknown";
                    //else
                    //    blog.UpdatedByName = _parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.UpdatedBy)?.Fullname ?? "Unknown";
                    blog.CreatedByName = _staffRepository.GetAllAsync().Result
                    .FirstOrDefault(s => s.Userid == blog.CreatedBy)?.Fullname ?? "Unknown";
                }

                if (blog.ApprovedBy != null)
                {
                    //if (_userRepository.GetByIdAsync(blog.ApprovedBy.Value).Result.IsStaff)
                    //    blog.ApprovedByName = _staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.ApprovedBy)?.Fullname ?? "Unknown";
                    //else
                    //    blog.ApprovedByName = _parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.ApprovedBy)?.Fullname ?? "Unknown";
                    blog.CreatedByName = _staffRepository.GetAllAsync().Result
                    .FirstOrDefault(s => s.Userid == blog.CreatedBy)?.Fullname ?? "Unknown";
                }
            }
            return blogDTOs;
        }
        public async Task<BlogDTO> GetBlogByIdAsync(int id)
        {
            var blogfindbyID = await _blogRepo.GetByIdAsync(id);
            BlogDTO blog = _mapper.Map<BlogDTO>(blogfindbyID);

            if (blog.CreatedBy != null && blog.CreatedBy != 0)
            {
                if (_userRepository.GetByIdAsync(blog.CreatedBy.Value).Result.IsStaff)
                    blog.CreatedByName = _staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.CreatedBy)?.Fullname ?? "Unknown";
                //else
                //    blog.CreatedByName = _parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.CreatedBy)?.Fullname ?? "Unknown";
            }
            else blog.CreatedByName = "Unknown";

            if (blog.UpdatedBy != null && blog.UpdatedBy != 0)
            {
                if (_userRepository.GetByIdAsync(blog.UpdatedBy.Value).Result.IsStaff)
                    blog.UpdatedByName = _staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.UpdatedBy)?.Fullname ?? "Unknown";
                //else
                //    blog.UpdatedByName = _parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.UpdatedBy)?.Fullname ?? "Unknown";
            }
            else blog.UpdatedByName = "Unknown";

            if (blog.ApprovedBy != null && blog.ApprovedBy != 0)
            {
                if (_userRepository.GetByIdAsync(blog.ApprovedBy.Value).Result.IsStaff)
                    blog.ApprovedByName = _staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.ApprovedBy)?.Fullname ?? "Unknown";
                //else
                //    blog.ApprovedByName = _parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.ApprovedBy)?.Fullname ?? "Unknown";
            }
            else blog.ApprovedByName = "Unknown";
            return blog;

        }
        public async Task<String> AddBlogAsync(CreateBlogDTO dto)
        {

            Blog blog = _mapper.Map<Blog>(dto);
            blog.Status = draft;
            blog.CreatedAt = DateTime.Now;
            string? imageUrl = null;
            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(dto.ImageFile.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                    throw new Exception("Only JPG and PNG and jpeg files are allowed.");
                if (dto.ImageFile.Length > 2 * 1024 * 1024)
                    throw new Exception("File size must be less than 2MB.");

                using var stream = dto.ImageFile.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(dto.ImageFile.FileName, stream),
                    Transformation = new Transformation().AspectRatio("16:9").Crop("fill")
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    imageUrl = uploadResult.SecureUrl.ToString();
                    blog.Image = uploadResult.SecureUrl.ToString();
                }
            }
            await _blogRepo.AddAsync(blog);
            _blogRepo.Save();
            return imageUrl;
        }
        public async Task<String> UpdateBlog(UpdateBlogDTO dto)
        {
            var entity = _blogRepo.GetByIdAsync(dto.BlogID).Result;
            string? imageUrl = null;
            if (dto != null)
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
                        if (dto.ImageFile != null && dto.ImageFile.Length > 0)
                        {
                            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                            var extension = Path.GetExtension(dto.ImageFile.FileName).ToLower();
                            if (!allowedExtensions.Contains(extension))
                                throw new Exception("Only JPG and PNG and jpeg files are allowed.");
                            if (dto.ImageFile.Length > 2 * 1024 * 1024)
                                throw new Exception("File size must be less than 2MB.");

                            using var stream = dto.ImageFile.OpenReadStream();
                            var uploadParams = new ImageUploadParams
                            {
                                File = new FileDescription(dto.ImageFile.FileName, stream),
                                Transformation = new Transformation().AspectRatio("16:9").Crop("fill")
                            };
                            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                imageUrl = uploadResult.SecureUrl.ToString();
                                entity.Image = uploadResult.SecureUrl.ToString();
                            }
                        }
                        _blogRepo.Update(entity);
                        _blogRepo.Save();
                    }
            }
            return imageUrl;

        }
        public async Task DeleteBlog(int id)
        {
            var entity = await _blogRepo.GetByIdAsync(id);
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
                if (blog.CreatedBy != null)
                {
                    if (_userRepository.GetByIdAsync(blog.CreatedBy.Value).Result.IsStaff)
                        blog.CreatedByName = _staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.CreatedBy)?.Fullname ?? "Unknown";
                    //else
                    //    blog.CreatedByName = _parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.CreatedBy)?.Fullname ?? "Unknown";
                }

                if (blog.UpdatedBy != null)
                {
                    if (_userRepository.GetByIdAsync(blog.UpdatedBy.Value).Result.IsStaff)
                        blog.UpdatedByName = _staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.UpdatedBy)?.Fullname ?? "Unknown";
                    //else
                    //    blog.UpdatedByName = _parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.UpdatedBy)?.Fullname ?? "Unknown";
                }

                if (blog.ApprovedBy != null)
                {
                    if (_userRepository.GetByIdAsync(blog.ApprovedBy.Value).Result.IsStaff)
                        blog.ApprovedByName = _staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.ApprovedBy)?.Fullname ?? "Unknown";
                    //else
                    //    blog.ApprovedByName = _parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.ApprovedBy)?.Fullname ?? "Unknown";
                }
            }
            return Task.FromResult(blogDTOs);

        }
        public async Task ApproveBlog(ApproveBlogDTO dto)
        {
            var blog = await _blogRepo.GetByIdAsync(dto.BlogId);
            if (blog != null)
            {
                blog.ApprovedBy = dto.ApprovedBy;
                blog.ApprovedOn = DateTime.Now;
                blog.Status = published;
                _blogRepo.Update(blog);
                _blogRepo.Save();
            }
        }
        public async Task<List<BlogDTO>> GetPublishedBlogs()
        {
            var blogs = (await _blogRepo.GetAllAsync()).Where(b =>
                    b.IsDeleted != true &&
                    //                    b.ApprovedBy != null &&
                    //                   b.ApprovedOn != null &&
                    b.Status != null &&
                    (b.Status.Equals(published, StringComparison.OrdinalIgnoreCase)))
                .ToList();
            List<BlogDTO> blogDTOs = _mapper.Map<List<BlogDTO>>(blogs);
            foreach (var blog in blogDTOs)
            {
                if (blog.CreatedBy != null)
                {
                        blog.CreatedByName = _staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.CreatedBy)?.Fullname ?? "Unknown";
                    //else
                    //    blog.CreatedByName = _parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.CreatedBy)?.Fullname ?? "Unknown";
                }

                if (blog.UpdatedBy != null)
                {
                        blog.UpdatedByName = _staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.UpdatedBy)?.Fullname ?? "Unknown";
                    //else
                    //    blog.UpdatedByName = _parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.UpdatedBy)?.Fullname ?? "Unknown";
                }

                if (blog.ApprovedBy != null)
                {
                        blog.ApprovedByName = _staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.ApprovedBy)?.Fullname ?? "Unknown";
                    //else
                    //    blog.ApprovedByName = _parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.ApprovedBy)?.Fullname ?? "Unknown";
                }
            }
            return blogDTOs;


        }
        public async Task RejectBlog(RejectBlogDTO dto)
        {
            var blog = await _blogRepo.GetByIdAsync(dto.BlogId);
            if (blog != null)
            {
                blog.ApprovedBy = dto.ApprovedBy;
                blog.ApprovedOn = DateTime.Now;
                blog.Status = rejected;
                _blogRepo.Update(blog);
                _blogRepo.Save();
            }
        }

        public Task<List<BlogDTO>> GetRejectedBlogs()
        {
            var blogs = _blogRepo.GetAllAsync().Result.Where(b =>
                    b.IsDeleted != true &&
                    b.Status != null &&
                    (b.Status.Equals(rejected, StringComparison.OrdinalIgnoreCase)))
                .ToList();
            List<BlogDTO> blogDTOs = _mapper.Map<List<BlogDTO>>(blogs);
            foreach (var blog in blogDTOs)
            {
                if (blog.CreatedBy != null)
                {
                        blog.CreatedByName = _staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.CreatedBy)?.Fullname ?? "Unknown";
                    //else
                    //    blog.CreatedByName = _parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.CreatedBy)?.Fullname ?? "Unknown";
                }

                if (blog.UpdatedBy != null)
                {
                        blog.UpdatedByName = _staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.UpdatedBy)?.Fullname ?? "Unknown";
                    //else
                    //    blog.UpdatedByName = _parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.UpdatedBy)?.Fullname ?? "Unknown";
                }

                if (blog.ApprovedBy != null)
                {
                        blog.ApprovedByName = _staffRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.ApprovedBy)?.Fullname ?? "Unknown";
                    //else
                    //    blog.ApprovedByName = _parentRepository.GetAllAsync().Result.FirstOrDefault(s => s.Userid == blog.ApprovedBy)?.Fullname ?? "Unknown";
                }
            }
            return Task.FromResult(blogDTOs);
        }

        /*        public async Task<string> UploadBlogImageAsync(BlogImageUploadDTO dto)
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
                }*/
    }
}
