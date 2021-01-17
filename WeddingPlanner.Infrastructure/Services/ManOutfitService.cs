using AutoMapper;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Repository.Abstractions;
using WeddingPlanner.Infrastructure.Services.Abstractions;

namespace WeddingPlanner.Infrastructure.Services
{
    public class ManOutfitService : BaseOutfitService<ManOutfitDto, ManOutfit, IOutfitRepository<ManOutfit>>
    {
        public ManOutfitService(
            IOutfitRepository<ManOutfit> manOutfitRepository,
            IMapper mapper) : base(manOutfitRepository, mapper)
        { }
    }
}