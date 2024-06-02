using AutoMapper;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    public class PromoCodeMappingProfile : Profile
    {
        public PromoCodeMappingProfile()
        {
            CreateMap<PromoCodeCreateDto, PromoCode>();

            CreateMap<PromoCode, PromoCodeReadDto>();

            CreateMap<PromoCodeUpdateDto, PromoCode>();
        }
    }
}
