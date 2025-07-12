using AutoMapper;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Blogs;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogDTO>()
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.CreatedByUserId).Fullname))
                .ForMember(dest => dest.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedByUser.StaffUsers.FirstOrDefault(s => s.Userid == src.ModifiedByUserId).Fullname))
                .ForMember(dest => dest.ApprovedByUserName, opt => opt.MapFrom(src => src.ApprovedByNavigation.Fullname))

                .ReverseMap();
            CreateMap<CreateBlogDTO, Blog>().ReverseMap();
            CreateMap<UpdateBlogDTO, Blog>().ReverseMap();
            CreateMap<Blog, ApproveBlogDTO>().ReverseMap();
            CreateMap<BlogImageUploadDTO, Blog>().ReverseMap();
        }
    }
}
