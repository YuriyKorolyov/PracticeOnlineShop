using AutoMapper;
using MyApp.Dto.CreateDto;
using MyApp.Dto.ReadDto;
using MyApp.Models;

namespace MyApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductReadDto>().ReverseMap();
            CreateMap<User, UserReadDto>().ReverseMap();
            CreateMap<Role, RoleReadDto>().ReverseMap();
            CreateMap<Category, CategoryReadDto>().ReverseMap();
            CreateMap<Cart, CartReadDto>().ReverseMap();
            CreateMap<Order, OrderReadDto>().ReverseMap();
            CreateMap<Payment, PaymentReadDto>().ReverseMap();
            CreateMap<Review, ReviewReadDto>().ReverseMap();
            CreateMap<ViewHistory, ViewHistoryReadDto>().ReverseMap();

            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<Role, RoleCreateDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Cart, CartCreateDto>().ReverseMap();
            CreateMap<Order, OrderCreateDto>().ReverseMap();
            CreateMap<Payment, PaymentCreateDto>().ReverseMap();
            CreateMap<Review, ReviewCreateDto>().ReverseMap();
            CreateMap<ViewHistory, ViewHistoryCreateDto>().ReverseMap();
        }
    }
}
