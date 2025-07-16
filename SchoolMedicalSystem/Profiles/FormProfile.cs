using AutoMapper;
using DataAccessLayer.DTO.Form;
using DataAccessLayer.Entity;

namespace SchoolMedicalSystem.Profiles
{
    public class FormProfile : Profile
    {
        public FormProfile()
        {
            CreateMap<Form, FormDTO>()
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent.Fullname))
                .ForMember(dest => dest.FormCategoryName, opt => opt.MapFrom(src => src.Formcategory.Categoryname))
                .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.Staff != null ? src.Staff.Fullname : ""))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.FormNavigation.Fullname))
                .ReverseMap();

            CreateMap<CreateFormDTO, Form>().ReverseMap();
            CreateMap<UpdateFormDTO, Form>().ReverseMap();
            CreateMap<Form, ResponseFormDTO>()
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent.Fullname))
                .ForMember(dest => dest.ParentEmail, opt => opt.MapFrom(src => src.Parent.Email));
            CreateMap<AddFormMedicine,Form>()
                .ForMember(src => src.Reason, opt => opt.MapFrom(form =>
                $"Tên thuốc: {form.MedicineName}\n" +
                $"Chi tiết thuốc: {form.MedicineDescription}\n" +
                $"Lí do: {form.Reason}"))
                .ForSourceMember(src => src.MedicineName, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.MedicineDescription, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.Reason, opt => opt.DoNotValidate())
                .ForMember(src => src.FormcategoryId, opt => opt.MapFrom(form => 3))
                ;

        }
    }
}
