using WeddingPlanner.Api.Models;
using WeddingPlanner.Infrastructure.Dto;

namespace WeddingPlanner.Api.Services.Abstractions
{
    public interface IOutfitService : IBaseService<OutfitDto, OutfitResponse<OutfitDto>>
    { }
}
