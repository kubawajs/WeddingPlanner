using AutoMapper;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Guest, GuestDto>();
            CreateMap<GuestDto, Guest>();
            CreateMap<WeddingHall, WeddingHallDto>();
            CreateMap<WeddingHallDto, WeddingHall>();
        }
    }
}
