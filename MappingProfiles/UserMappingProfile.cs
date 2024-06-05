using AutoMapper;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    /// <summary>
    /// Профиль отображения для пользователей.
    /// </summary>
    public class UserMappingProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UserMappingProfile"/>.
        /// </summary>
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
