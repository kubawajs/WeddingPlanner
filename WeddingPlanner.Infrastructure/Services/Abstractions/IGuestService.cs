using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Dto;

namespace WeddingPlanner.Infrastructure.Services.Abstractions
{
    public interface IGuestService
    {
        Task<IEnumerable<GuestDto>> GetGuestsAsync();
        Task CreateGuestAsync(GuestDto guestDto);
    }
}
