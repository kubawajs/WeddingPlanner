using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Data;
using WeddingPlanner.Infrastructure.Repository.Abstractions;

namespace WeddingPlanner.Infrastructure.Repository
{
    public class WeddingHallRepository : IWeddingHallRespository
    {
        private readonly WeddingPlannerDbContext _context;

        public WeddingHallRepository(WeddingPlannerDbContext context)
        {
            _context = context;
        }

        public async Task<WeddingHall> GetWeddingHallAsync(int id)
        {
            var weddingHall = await _context.WeddingHalls.Include(x => x.AdditionalCosts)
                                                         .FirstOrDefaultAsync(x => x.Id == id);
            if(weddingHall == null)
            {
                throw new Exception($"Item with id {id} does not exist.");
            }

            return weddingHall;
        }

        public async Task CreateWeddingHallAsync(WeddingHall weddingHall)
        {
            _context.WeddingHalls.Add(weddingHall);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WeddingHall>> GetWeddingHallsAsync()
        {
            return await _context.WeddingHalls.Include(x => x.AdditionalCosts).ToListAsync();
        }

        public async Task UpdateWeddingHallAsync(WeddingHall weddingHall)
        {
            var entityToUpdate = await _context.WeddingHalls.Include(x => x.AdditionalCosts)
                                                            .FirstOrDefaultAsync(x => x.Id == weddingHall.Id);
            if (entityToUpdate == null)
            {
                throw new DbUpdateException($"Cannot update item: specified item id: {weddingHall.Id} does not exist.");
            }

            _context.Entry(entityToUpdate).CurrentValues.SetValues(weddingHall);
        }
    }
}
