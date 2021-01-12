using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Core.Repositories;

namespace WeddingPlanner.Infrastructure.Repository.Abstractions
{
    public interface IWeddingHallRespository : IRepository
    {
        Task<WeddingHall> GetWeddingHallAsync(int id);
        Task UpdateWeddingHallAsync(WeddingHall weddingHall);
        Task CreateWeddingHallAsync(WeddingHall weddingHall);
    }
}
