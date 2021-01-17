using System.Threading.Tasks;
using WeddingPlanner.Core.Services;
using WeddingPlanner.Infrastructure.Dto.Abstractions;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Infrastructure.Services.Abstractions
{
    public interface IOutfitService<TDto> : IService
        where TDto : BaseOutfitDto 
    {
        Task<OutfitResponse<TDto>> CreateOutfitAsync(TDto model);
        Task<OutfitResponse<TDto>> UpdateOutfitAsync(TDto model);
        Task<OutfitResponse<TDto>> GetOutfitAsync(int id);
    }
}
