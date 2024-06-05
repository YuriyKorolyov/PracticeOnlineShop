using AutoMapper;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    /// <summary>
    /// Профиль отображения для сущности промокода.
    /// </summary>
    public class PromoCodeMappingProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PromoCodeMappingProfile"/>.
        /// </summary>
        public PromoCodeMappingProfile()
        {
            CreateMap<PromoCodeCreateDto, PromoCode>();

            CreateMap<PromoCode, PromoCodeReadDto>();

            CreateMap<PromoCodeUpdateDto, PromoCode>();
        }
    }
}
