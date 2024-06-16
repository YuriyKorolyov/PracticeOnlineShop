using AutoMapper;
using MyApp.Dto.ExportToExcel;
using MyApp.Dto.Read;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    public class ProductCategoryMappingProfile : Profile
    {
        public ProductCategoryMappingProfile()
        {

            CreateMap<ProductCategory, ProductCategoryExcelDto>();
        }
    }
}
