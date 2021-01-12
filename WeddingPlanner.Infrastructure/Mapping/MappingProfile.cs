using AutoMapper;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Dto;

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
