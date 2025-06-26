using AutoMapper;

namespace SchoolMedicalSystem.Profiles
{
    public class MedicineDonationProfile : Profile
    {
        public MedicineDonationProfile()
        {
            CreateMap<DataAccessLayer.DTO.MedicineDonationDto, DataAccessLayer.Entity.Medicinedonation>();
        }
    }
}
