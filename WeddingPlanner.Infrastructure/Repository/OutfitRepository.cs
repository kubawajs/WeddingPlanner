using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Data;
using WeddingPlanner.Infrastructure.Repository.Abstractions;

namespace WeddingPlanner.Infrastructure.Repository
{
    public class OutfitRepository : BaseRepository<Outfit>, IOutfitRepository
    {
        public OutfitRepository(WeddingPlannerDbContext context)
            : base(context)
        { }

        public override async Task CreateAsync(Outfit outfit)
        {
            Context.Outfits.Add(outfit);
            await Context.SaveChangesAsync();
        }

        public override async Task<IEnumerable<Outfit>> GetAllAsync()
        {
            return await Context.Outfits.ToListAsync();
        }

        public override async Task<Outfit> GetAsync(int id) 
            => await Context.Outfits.Include(x => x.Costs)
                                    .FirstOrDefaultAsync(x => x.Id == id);
    }
}
