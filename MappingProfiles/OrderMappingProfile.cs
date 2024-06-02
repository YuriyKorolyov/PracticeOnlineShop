using AutoMapper;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderReadDto>();

            CreateMap<OrderUpdateDto, Order>();

            CreateMap<OrderDetail,  OrderDetailReadDto>();
        }
    }
}
