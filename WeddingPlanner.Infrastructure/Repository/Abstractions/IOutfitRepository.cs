using WeddingPlanner.Core.Domain.Abstractions;

namespace WeddingPlanner.Infrastructure.Repository.Abstractions
{
    public interface IOutfitRepository<TModel> : IBaseRepository<TModel>
        where TModel : BaseOutfit
    {
    }
}
