using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    public class PaymentMappingProfile : Profile
    {
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
