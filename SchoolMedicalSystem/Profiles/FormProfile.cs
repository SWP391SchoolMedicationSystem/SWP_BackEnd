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

            CreateMap<UpdateFormDTO, Form>().ReverseMap();
            CreateMap<Form, ResponseFormDTO>().ReverseMap();
            CreateMap<AddFormMedicine,Form>()
                .ForMember(src => src.Reason, opt => opt.MapFrom(form =>
                $"Tên thuốc: {form.MedicineName}\n" +
                $"Chi tiết thuốc: {form.MedicineDescription}\n" +
                $"Lí do: {form.Reason}"))
                .ForSourceMember(src => src.MedicineName, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.MedicineDescription, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.Reason, opt => opt.DoNotValidate())
                .ForMember(src => src.FormcategoryId, opt => opt.MapFrom(form => 2))
                ;
            CreateMap<AddFormAbsent, Form>()
                .ForMember(src => src.Reason, opt => opt.MapFrom(form =>
                $"Lí do vắng: {form.ReasonForAbsent}\n" +
                $"Ngày vắng: {form.AbsentDate}"))
                .ForSourceMember(src => src.ReasonForAbsent, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.AbsentDate, opt => opt.DoNotValidate())
                .ForMember(src => src.FormcategoryId, opt => opt.MapFrom(form => 1))
                ;
            CreateMap<AddFormChronicIllness, Form>()
                .ForMember(src => src.Reason, opt => opt.MapFrom(form =>
                $"Tên bệnh: {form.ChronicIllnessName}\n" +
                $"Chi tiết bệnh: {form.ChronicIllnessDescription}\n" +
                $"Triệu chứng: {form.Systoms}\n" +
                $"Hành động cần thực hiện: {form.ActionRequired}"))
                .ForSourceMember(src => src.ChronicIllnessName, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.ChronicIllnessDescription, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.Systoms, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.ActionRequired, opt => opt.DoNotValidate())
                .ForMember(src => src.FormcategoryId, opt => opt.MapFrom(form => 3))
                ;
            CreateMap<AddFormPhysicalActivityModification, Form>()
                .ForMember(src => src.Reason, opt => opt.MapFrom(form =>
                $"Lí do thay đổi: {form.ReasonForModification}\n" +
                $"Chi tiết thay đổi: {form.ModificationDetails}"))
                .ForSourceMember(src => src.ReasonForModification, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.ModificationDetails, opt => opt.DoNotValidate())
                .ForMember(src => src.FormcategoryId, opt => opt.MapFrom(form => 4))
                ;
            CreateMap<CreateFormDTO, Form>()
                .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.Reason))
                .ForMember(dest => dest.FormcategoryId, opt => opt.MapFrom(src => 5))
                .ReverseMap();// Assuming 5 is the ID for the general form category


        }
    }
}
