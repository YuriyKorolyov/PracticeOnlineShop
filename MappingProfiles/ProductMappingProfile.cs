using AutoMapper;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    /// <summary>
    /// Профиль отображения для сущности продукта.
    /// </summary>
    public class ProductMappingProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ProductMappingProfile"/>.
        /// </summary>
        public ProductMappingProfile()
        {
            CreateMap<ProductCreateDto, Product>();

            CreateMap<Product, ProductReadDto>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)));
            
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
