using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Core.Domain.Abstractions;
using WeddingPlanner.Core.Repositories;

namespace WeddingPlanner.Infrastructure.Repository.Abstractions
{
    public interface IOutfitRepository<TModel> : IRepository
        where TModel : BaseOutfit
    {
        Task<TModel> GetOutfitAsync(int id);
        Task CreateOutfitAsync(TModel outfit);
        Task UpdateOutfitAsync(TModel outfit);
    }
}
