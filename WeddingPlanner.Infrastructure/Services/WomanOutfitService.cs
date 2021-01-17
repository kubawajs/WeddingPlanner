using AutoMapper;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Repository.Abstractions;
using WeddingPlanner.Infrastructure.Services.Abstractions;

namespace WeddingPlanner.Infrastructure.Services
{
    public class WomanOutfitService : BaseOutfitService<WomanOutfitDto, WomanOutfit, IOutfitRepository<WomanOutfit>>
    {
        public WomanOutfitService(
            IOutfitRepository<WomanOutfit> womanOutfitRepository,
            IMapper mapper) : base(womanOutfitRepository, mapper)
        { }
    }
}
