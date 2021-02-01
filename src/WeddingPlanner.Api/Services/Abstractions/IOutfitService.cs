using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Api.Services.Abstractions
{
    public interface IOutfitService : IBaseService<OutfitDto, OutfitResponse<OutfitDto>>
    { }
}
