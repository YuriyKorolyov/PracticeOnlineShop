﻿using AutoMapper;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    /// <summary>
    /// Профиль отображения для отзывов.
    /// </summary>
    public class ReviewMappingProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ReviewMappingProfile"/>.
        /// </summary>
        public ReviewMappingProfile()
        {
            CreateMap<ReviewCreateDto, Review>()
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
            
            CreateMap<Review, ReviewReadDto>();

            CreateMap<ReviewUpdateDto, Review>()
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}
