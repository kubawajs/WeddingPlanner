using System.Threading.Tasks;
using WeddingPlanner.Core.Services;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Infrastructure.Services.Abstractions
{
    public interface IWeddingHallService : IService
    {
        Task<WeddingHallSummaryResponse> GetWeddingHallSummary(int? id);
        Task<WeddingHallSummaryResponse> CreateWeddingHall(WeddingHallDto model);
        Task<WeddingHallSummaryResponse> UpdateWeddingHall(WeddingHallDto model);
    }
}
