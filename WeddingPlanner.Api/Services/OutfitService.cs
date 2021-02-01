using AutoMapper;
using WeddingPlanner.Api.Services.Abstractions;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Models.Abstractions;
using WeddingPlanner.Infrastructure.Repository.Abstractions;

namespace WeddingPlanner.Api.Services
{
    public class OutfitService
        : BaseService<OutfitDto, Outfit, IOutfitRepository, OutfitResponse<OutfitDto>>, IOutfitService
    {
        public OutfitService(
            IOutfitRepository manOutfitRepository,
            IMapper mapper)
            : base(manOutfitRepository, mapper)
        { }

        protected override OutfitResponse<OutfitDto> CreateErrorResponse(string message)
        {
            return new OutfitResponse<OutfitDto>(
                BaseApiResponse<OutfitDto>.CreateErrorResponse(message));
        }

        protected override OutfitResponse<OutfitDto> CreateSuccessResponse(string message, OutfitDto item)
        {
            return new OutfitResponse<OutfitDto>(
                BaseApiResponse<OutfitDto>.CreateSuccessResponse(message, item));
        }
    }
}