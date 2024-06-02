using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Models;
using AutoMapper;

namespace MyApp.MappingProfiles
{   
    public class CartMappingProfile : Profile
    {
        public CartMappingProfile()
        {
            CreateMap<CartCreateDto, Cart>()
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Cart, CartReadDto>();

            CreateMap<CartUpdateDto, Cart>()
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}
