using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain.Abstractions;
using WeddingPlanner.Core.Repositories;

namespace WeddingPlanner.Infrastructure.Repository.Abstractions
{
    public interface IBaseRepository<TModel> : IRepository
        where TModel : BaseModel
    {
        public Task CreateAsync(TModel model);
        public Task<TModel> GetAsync(int id);
        Task<IEnumerable<TModel>> GetAllAsync();
        public Task UpdateAsync(TModel model);
    }
}
