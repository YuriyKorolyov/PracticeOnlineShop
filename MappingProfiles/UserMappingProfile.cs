using AutoMapper;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserCreateDto, User>()
            .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<User, UserReadDto>();

            CreateMap<UserUpdateDto, User>()
            .ForMember(dest => dest.Role, opt => opt.Ignore());
        }
    }
}
