using AutoMapper;
using DataAccessLayer.DTO.StudentSpecialNeeds;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class StudentSpecialNeedProfile : Profile
    {
        public StudentSpecialNeedProfile()
        {
            CreateMap<StudentSpecialNeedDTO, StudentSpecialNeed>()
                .ReverseMap();
            CreateMap<CreateSpecialStudentNeedDTO, StudentSpecialNeed>().ReverseMap();
            CreateMap<UpdateStudentSpecialNeedDTO, StudentSpecialNeed>().ReverseMap();
        }
    }
}
