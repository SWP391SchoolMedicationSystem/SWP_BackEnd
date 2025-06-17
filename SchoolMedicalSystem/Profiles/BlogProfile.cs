using AutoMapper;
using DataAccessLayer.DTO.Blogs;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogDTO>().ReverseMap();
            CreateMap<BlogDTO, Blog>().ReverseMap();
        }
    }
}
