using AutoMapper;
using FlagsApi.Dtos;

namespace FlagsApi.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDto>()
                .ReverseMap();

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName, map => map.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ReverseMap();
        }
    }
}
