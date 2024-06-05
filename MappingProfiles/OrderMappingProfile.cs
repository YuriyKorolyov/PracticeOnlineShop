using AutoMapper;
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

            CreateMap<OrderDetail,  OrderDetailReadDto>();
        }
    }
}
