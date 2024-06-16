using AutoMapper;
using MyApp.Dto.Create;
using MyApp.Dto.ExportToExcel;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    /// <summary>
    /// Профиль отображения для истории просмотров.
    /// </summary>
    public class ViewHistoryMappingProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ViewHistoryMappingProfile"/>.
        /// </summary>
        public ViewHistoryMappingProfile()
        {
            CreateMap<ViewHistoryCreateDto, ViewHistory>()
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<ViewHistory, ViewHistoryReadDto>();

            CreateMap<ViewHistoryUpdateDto, ViewHistory>()
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<ViewHistory, ViewHistoryExcelDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));
        }
    }
}
