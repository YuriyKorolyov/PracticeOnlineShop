using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Models;

namespace MyApp.MappingProfiles
{
    public class ViewHistoryMappingProfile : Profile
    {
        public ViewHistoryMappingProfile()
        {
            CreateMap<ViewHistoryCreateDto, ViewHistory>()
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<ViewHistory, ViewHistoryReadDto>();

            CreateMap<ViewHistoryUpdateDto, ViewHistory>()
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}
