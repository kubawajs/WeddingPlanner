using WeddingPlanner.Api.Models;
using WeddingPlanner.Infrastructure.Dto;

namespace WeddingPlanner.Api.Services.Abstractions
{
    public interface IWeddingHallService : IBaseService<WeddingHallDto, WeddingHallSummaryResponse>
    { }
}
