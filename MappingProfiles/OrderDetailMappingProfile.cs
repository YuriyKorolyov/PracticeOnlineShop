using AutoMapper;
using MyApp.Dto.ExportToExcel;
using MyApp.Dto.Read;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    public class OrderDetailMappingProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="OrderDetailMappingProfile"/>.
        /// </summary>
        public OrderDetailMappingProfile()
        {
            CreateMap<OrderDetail, OrderDetailReadDto>();

            CreateMap<OrderDetail, OrderDetailExcelDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id));
        }
    }
}
