using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Core.Repositories;

namespace WeddingPlanner.Infrastructure.Repository.Abstractions
{
    public interface IGuestRepository : IRepository
    {
        Task<IEnumerable<Guest>> GetGuestsAsync();
        Task CreateGuestAsync(Guest guestDto);
        Task<int> GetAllGuestsCountAsync(int age);
        Task<IEnumerable<Guest>> GetGuestsByAgeAsync(int age);
        Task<int> GetChildGuestsCountAsync(int childAgeFrom, int childAgeTo);
    }
}
