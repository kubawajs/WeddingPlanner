using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Infrastructure.Services.Abstractions
{
    public interface IWeddingHallService : IBaseService<WeddingHallDto, WeddingHallSummaryResponse>
    { }
}
