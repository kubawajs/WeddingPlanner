using AutoMapper;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Guest, GuestDto>().ReverseMap();
            CreateMap<WeddingHall, WeddingHallDto>().ReverseMap();
            CreateMap<ManOutfit, ManOutfitDto>().ReverseMap();
            CreateMap<WomanOutfit, WomanOutfitDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
