using AutoMapper;
using MyApp.Dto.Account;
using MyApp.Dto.Create;
using MyApp.Dto.ExportToExcel;
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
            CreateMap<UserCreateDto, User>();

            CreateMap<User, UserReadDto>();

            CreateMap<UserUpdateDto, User>();

            CreateMap<User, UserExcelDto>();

            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => DateTime.Now));

        }
    }
}
