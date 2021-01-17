using WeddingPlanner.Infrastructure.Dto.Abstractions;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Infrastructure.Services.Abstractions
{
    public interface IOutfitService<TDto> : IBaseService<TDto, OutfitResponse<TDto>>
        where TDto : BaseOutfitDto
    {
    }
}
