using AutoMapper;
using MyApp.Dto;
using MyApp.Models;

namespace MyApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<CartDto, Cart>();
            CreateMap<Cart,  CartDto>();
        }
    }
}
