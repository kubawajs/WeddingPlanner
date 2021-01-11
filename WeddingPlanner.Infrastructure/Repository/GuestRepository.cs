using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Data;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Repository.Abstractions;

namespace WeddingPlanner.Infrastructure.Repository
{
    public class GuestRepository : IGuestRepository
    {
        private readonly WeddingPlannerDbContext _context;

        public GuestRepository(WeddingPlannerDbContext context)
        {
            _context = context;
        }

        public async Task CreateGuestAsync(Guest guest)
        {
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Guest>> GetGuestsAsync()
        {
            return await _context.Guests.ToListAsync();
        }

        public async Task<IEnumerable<Guest>> GetGuestsByAgeAsync(int age)
        {
            return await _context.Guests.Where(x => x.Age >= age || !x.IsChild).ToListAsync();
        }

        public async Task<int> GetAllGuestsCountAsync(int age = 0)
        {
            var guestsCount = await _context.Guests.Where(x => x.Age > age || !x.IsChild).CountAsync();
            var partnersCount = await _context.Guests.Where(x => x.HasPair == true && !x.IsChild).CountAsync();

            return await Task.FromResult(partnersCount + guestsCount);
        }

        public async Task<int> GetChildGuestsCountAsync(int childAgeFrom, int childAgeTo)
        {
            return await _context.Guests.Where(x => x.Age >= childAgeFrom && x.Age <= childAgeTo).CountAsync();
        }
    }
}
