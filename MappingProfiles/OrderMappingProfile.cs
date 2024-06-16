using AutoMapper;
using MyApp.Dto.ExportToExcel;
using MyApp.Dto.Read;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    /// <summary>
    /// Профиль отображения для сущности заказа.
    /// </summary>
    public class OrderMappingProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="OrderMappingProfile"/>.
        /// </summary>
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderReadDto>();

            CreateMap<Order, OrderExcelDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
