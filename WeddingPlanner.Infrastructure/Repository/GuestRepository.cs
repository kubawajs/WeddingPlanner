using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            // TODO: consider how to assign Id automatically
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Guest>> GetGuestsAsyns()
        {
            return await _context.Guests.ToListAsync();
        }
    }
}
