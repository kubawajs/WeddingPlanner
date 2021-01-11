using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Infrastructure.Repository.Abstractions
{
    public interface IWeddingHallRespository
    {
        Task<WeddingHall> GetWeddingHallAsync(int id);
        Task UpdateWeddingHallAsync(WeddingHall weddingHall);
        Task CreateWeddingHallAsync(WeddingHall weddingHall);
    }
}
