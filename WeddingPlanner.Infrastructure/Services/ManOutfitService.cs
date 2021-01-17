using AutoMapper;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Models.Abstractions;
using WeddingPlanner.Infrastructure.Repository.Abstractions;
using WeddingPlanner.Infrastructure.Services.Abstractions;

namespace WeddingPlanner.Infrastructure.Services
{
    public class ManOutfitService
        : BaseService<ManOutfitDto, ManOutfit, IOutfitRepository<ManOutfit>, OutfitResponse<ManOutfitDto>>, IOutfitService<ManOutfitDto>
    {
        public ManOutfitService(
            IOutfitRepository<ManOutfit> manOutfitRepository,
            IMapper mapper) : base(manOutfitRepository, mapper)
        { }

        protected override OutfitResponse<ManOutfitDto> CreateErrorResponse(string message)
        {
            return new OutfitResponse<ManOutfitDto>(
                BaseApiResponse<ManOutfitDto>.CreateErrorResponse(message));
        }

        protected override OutfitResponse<ManOutfitDto> CreateSuccessResponse(string message, ManOutfitDto item)
        {
            return new OutfitResponse<ManOutfitDto>(
                BaseApiResponse<ManOutfitDto>.CreateSuccessResponse(message, item));
        }
    }
}