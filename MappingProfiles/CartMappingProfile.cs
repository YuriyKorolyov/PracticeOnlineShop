﻿using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Models;
using AutoMapper;
using MyApp.Dto.ExportToExcel;

namespace MyApp.MappingProfiles
{
    /// <summary>
    /// Профиль отображения для сущности корзины.
    /// </summary>
    public class CartMappingProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CartMappingProfile"/>.
        /// </summary>
        public CartMappingProfile()
        {
            CreateMap<CartCreateDto, Cart>()
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Cart, CartReadDto>();

            CreateMap<CartUpdateDto, Cart>()
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Cart, CartExcelDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));
        }
    }
}
