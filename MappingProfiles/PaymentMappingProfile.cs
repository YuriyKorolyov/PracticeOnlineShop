using AutoMapper;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    /// <summary>
    /// Профиль отображения для сущности платежа.
    /// </summary>
    public class PaymentMappingProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PaymentMappingProfile"/>.
        /// </summary>
        public PaymentMappingProfile()
        {
            CreateMap<PaymentCreateDto, Payment>()
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.PromoCode, opt => opt.Ignore());

            CreateMap<Payment, PaymentReadDto>();

            CreateMap<PaymentUpdateDto, Payment>()
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.PromoCode, opt => opt.Ignore());
        }
    }
}
