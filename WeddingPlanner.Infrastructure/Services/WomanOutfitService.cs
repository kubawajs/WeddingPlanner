using AutoMapper;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Models.Abstractions;
using WeddingPlanner.Infrastructure.Repository.Abstractions;
using WeddingPlanner.Infrastructure.Services.Abstractions;

namespace WeddingPlanner.Infrastructure.Services
{
    public class WomanOutfitService : BaseService<WomanOutfitDto, WomanOutfit, IOutfitRepository<WomanOutfit>, OutfitResponse<WomanOutfitDto>>, IOutfitService<WomanOutfitDto>
    {
        public WomanOutfitService(
            IOutfitRepository<WomanOutfit> womanOutfitRepository,
            IMapper mapper) : base(womanOutfitRepository, mapper)
        { }

        protected override OutfitResponse<WomanOutfitDto> CreateErrorResponse(string message)
        {
            return new OutfitResponse<WomanOutfitDto>(
                BaseApiResponse<WomanOutfitDto>.CreateErrorResponse(message));
        }

        protected override OutfitResponse<WomanOutfitDto> CreateSuccessResponse(string message, WomanOutfitDto item)
        {
            return new OutfitResponse<WomanOutfitDto>(
                BaseApiResponse<WomanOutfitDto>.CreateSuccessResponse(message, item));
        }
    }
}