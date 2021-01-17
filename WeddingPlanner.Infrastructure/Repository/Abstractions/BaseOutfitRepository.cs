using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain.Abstractions;
using WeddingPlanner.Infrastructure.Data;

namespace WeddingPlanner.Infrastructure.Repository.Abstractions
{
    public abstract class BaseOutfitRepository<TModel> : IOutfitRepository<TModel>
        where TModel : BaseOutfit
    {
        protected readonly WeddingPlannerDbContext Context;

        public BaseOutfitRepository(WeddingPlannerDbContext context)
        {
            Context = context;
        }

        public abstract Task CreateOutfitAsync(TModel outfit);
        public abstract Task<TModel> GetOutfitAsync(int id);
        public async virtual Task UpdateOutfitAsync(TModel outfit)
        {
            var entityToUpdate = await GetOutfitAsync(outfit.Id);
            if (entityToUpdate == null)
            {
                throw new DbUpdateException($"Cannot update item: specified item id: {outfit.Id} does not exist.");
            }

            Context.Entry(entityToUpdate).CurrentValues.SetValues(outfit);
            await Context.SaveChangesAsync();
        }
    }
}
