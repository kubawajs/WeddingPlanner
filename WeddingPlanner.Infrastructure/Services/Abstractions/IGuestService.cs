using System.Threading.Tasks;
using WeddingPlanner.Core.Services;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Infrastructure.Services.Abstractions
{
    public interface IGuestService : IService
    {
        Task<GuestListResponse> GetGuestsAsync();
        Task<GuestResponse> CreateGuestAsync(GuestDto guestDto);
        Task<GuestListResponse> GetGuestsByAgeAsync(int age);
    }
}
