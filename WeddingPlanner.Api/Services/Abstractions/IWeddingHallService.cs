using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Api.Services.Abstractions
{
    public interface IWeddingHallService : IBaseService<WeddingHallDto, WeddingHallSummaryResponse>
    { }
}
