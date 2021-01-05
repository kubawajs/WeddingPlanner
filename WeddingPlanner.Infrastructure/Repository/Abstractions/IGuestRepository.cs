using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Infrastructure.Repository.Abstractions
{
    public interface IGuestRepository
    {
        Task<IEnumerable<Guest>> GetGuestsAsyns();
        Task CreateGuestAsync(Guest guestDto);
    }
}
