using PatientCare.Repository.Models;
using PatientCare.Services.DTO;

namespace PatientCare.Services.AutoMapperProfiles
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<PatientDto, Patient>()
                .ForCtorParam("createDate", opt=> opt.MapFrom(src=> src.CreatedDate))
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ReverseMap();
            
            this.CreateMap<ImmunisationDto, Immunisation>()
                .ReverseMap();
        }
    }
}
