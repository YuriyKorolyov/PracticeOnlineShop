using AutoMapper;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryCreateDto, Category>();

            CreateMap<ProductCategory, CategoryReadDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

            CreateMap<Category, CategoryReadDto>();

            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
