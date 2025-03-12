using AutoMapper;
using LVCore.LVApp.Shared.Dtos.ResponseDtos.StockDtos;
using LVCore.LVApp.Shared.Entities;
using LVCore.LVApp.Shared.Pagination;

namespace LVCore.LVApp.BusinessService.AutoMapper.Profiles
{
    public class StockMappingProfile : Profile
    {
        public StockMappingProfile()
        {
            CreateMap<Stock, StockResponseDto>().ReverseMap();
            CreateMap<PaginatedList<Stock>, PaginatedList<StockResponseDto>>().ReverseMap();
        }
    }
}
