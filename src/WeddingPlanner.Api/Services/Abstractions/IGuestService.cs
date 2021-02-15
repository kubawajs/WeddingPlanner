using System.Threading.Tasks;
using WeddingPlanner.Api.Models;
using WeddingPlanner.Infrastructure.Dto;

namespace WeddingPlanner.Api.Services.Abstractions
{
    public interface IGuestService : IBaseService<GuestDto, GuestResponse>
    {
        Task<GuestListResponse> GetGuestsAsync();
        Task<GuestListResponse> GetGuestsByAgeAsync(int age);
    }
}
