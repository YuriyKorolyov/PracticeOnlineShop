using AutoMapper;
using MyApp.Dto.Create;
using MyApp.Dto.ExportToExcel;
using MyApp.Dto.Read;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    /// <summary>
    /// Профиль отображения для ролей.
    /// </summary>
    public class RoleMappingProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RoleMappingProfile"/>.
        /// </summary>
        public RoleMappingProfile()
        {
            CreateMap<RoleCreateDto, Role>();

            CreateMap<Role, RoleReadDto>();

            CreateMap<Role, RoleExcelDto>();
        }
    }
}
