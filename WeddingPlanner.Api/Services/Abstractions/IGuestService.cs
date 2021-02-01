using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Api.Services.Abstractions
{
    public interface IGuestService : IBaseService<GuestDto, GuestResponse>
    {
        Task<GuestListResponse> GetGuestsAsync();
        Task<GuestListResponse> GetGuestsByAgeAsync(int age);
    }
}
