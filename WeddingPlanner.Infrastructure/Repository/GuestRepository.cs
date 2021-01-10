using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<IEnumerable<Guest>> GetGuestsByAge(int age)
        {
            var cutOffDate = DateTime.Now.AddYears(-age);
            return await _context.Guests.Where(x => x.BirthDate <= cutOffDate).ToListAsync();
        }

        public async Task<int> GetGuestsCount()
        {
            return await _context.Guests.CountAsync();
        }
    }
}
