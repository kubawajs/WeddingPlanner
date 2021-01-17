using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain.Abstractions;
using WeddingPlanner.Infrastructure.Data;

namespace WeddingPlanner.Infrastructure.Repository.Abstractions
{
    public abstract class BaseRepository<TModel> : IBaseRepository<TModel>
        where TModel : BaseModel
    {
        protected readonly WeddingPlannerDbContext Context;

        public BaseRepository(WeddingPlannerDbContext context)
        {
            Context = context;
        }

        public abstract Task CreateAsync(TModel model);
        public abstract Task<TModel> GetAsync(int id);
        public abstract Task<IEnumerable<TModel>> GetAllAsync();

        public async virtual Task UpdateAsync(TModel model)
        {
            var entityToUpdate = await GetAsync(model.Id);
            if (entityToUpdate == null)
            {
                throw new DbUpdateException($"Cannot update item: specified item id: {model.Id} does not exist.");
            }

            Context.Entry(entityToUpdate).CurrentValues.SetValues(model);
            await Context.SaveChangesAsync();
        }
    }
}
