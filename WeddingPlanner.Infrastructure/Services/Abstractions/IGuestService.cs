using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Infrastructure.Services.Abstractions
{
    public interface IGuestService
    {
        Task<GuestListResponse> GetGuestsAsync();
        Task CreateGuestAsync(GuestDto guestDto);
        Task<GuestListResponse> GetGuestsByAgeAsync(int age);
    }
}
