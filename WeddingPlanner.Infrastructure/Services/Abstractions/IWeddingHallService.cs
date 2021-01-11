using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Infrastructure.Services.Abstractions
{
    public interface IWeddingHallService
    {
        Task<WeddingHallSummaryResponse> GetWeddingHallSummary(int? id);
        Task<WeddingHallSummaryResponse> CreateWeddingHall(WeddingHallDto model);
        Task<WeddingHallSummaryResponse> UpdateWeddingHall(WeddingHallDto model);
    }
}
