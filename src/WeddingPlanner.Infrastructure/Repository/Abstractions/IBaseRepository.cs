using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain.Abstractions;
using WeddingPlanner.Core.Repositories;

namespace WeddingPlanner.Infrastructure.Repository.Abstractions
{
    public interface IBaseRepository<TModel> : IRepository
        where TModel : BaseModel
    {
        Task CreateAsync(TModel model);
        Task<TModel> GetAsync(int id);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task UpdateAsync(TModel model);
    }
}
