using AutoMapper;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<RoleCreateDto, Role>();

            CreateMap<Role, RoleReadDto>();

            CreateMap<RoleUpdateDto, Role>();
        }
    }
}
