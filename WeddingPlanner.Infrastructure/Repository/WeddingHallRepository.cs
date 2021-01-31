using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Data;
using WeddingPlanner.Infrastructure.Repository.Abstractions;

namespace WeddingPlanner.Infrastructure.Repository
{
    public class WeddingHallRepository : BaseRepository<WeddingHall>, IWeddingHallRespository
    {
        public WeddingHallRepository(WeddingPlannerDbContext context)
            : base(context)
        { }

        public override async Task<WeddingHall> GetAsync(int id)
        {
            var weddingHall = await Context.WeddingHalls.Include(x => x.CreatedBy)
                                                        .Include(x => x.UpdatedBy)
                                                        .Include(x => x.Costs)
                                                        .FirstOrDefaultAsync(x => x.Id == id);
            if(weddingHall == null)
            {
                throw new Exception($"Item with id {id} does not exist.");
            }

            return weddingHall;
        }

        public override async Task CreateAsync(WeddingHall weddingHall)
        {
            Context.WeddingHalls.Add(weddingHall);
            await Context.SaveChangesAsync();
        }

        public override async Task<IEnumerable<WeddingHall>> GetAllAsync()
        {
            return await Context.WeddingHalls.Include(x => x.CreatedBy)
                                             .Include(x => x.UpdatedBy)
                                             .Include(x => x.Costs)
                                             .ToListAsync();
        }
    }
}
