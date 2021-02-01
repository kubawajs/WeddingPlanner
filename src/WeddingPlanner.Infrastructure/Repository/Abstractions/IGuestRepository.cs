using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;

namespace WeddingPlanner.Infrastructure.Repository.Abstractions
{
    public interface IGuestRepository : IBaseRepository<Guest>
    {
        Task<int> GetAllGuestsCountAsync(int age);
        Task<IEnumerable<Guest>> GetGuestsByAgeAsync(int age);
        Task<int> GetChildGuestsCountAsync(int childAgeFrom, int childAgeTo);
    }
}
