using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Data;
using WeddingPlanner.Infrastructure.Repository.Abstractions;

namespace WeddingPlanner.Infrastructure.Repository
{
    public class ManOutfitRepository : BaseRepository<ManOutfit>, IOutfitRepository<ManOutfit>
    {
        public ManOutfitRepository(WeddingPlannerDbContext context)
            : base(context)
        { }

        public override async Task CreateAsync(ManOutfit outfit)
        {
            Context.ManOutfits.Add(outfit);
            await Context.SaveChangesAsync();
        }

        public override async Task<IEnumerable<ManOutfit>> GetAllAsync()
        {
            return await Context.ManOutfits.ToListAsync();
        }

        public override async Task<ManOutfit> GetAsync(int id) 
            => await Context.ManOutfits.Include(x => x.AdditionalCosts)
                                       .Include(x => x.Shoes)
                                       .Include(x => x.Suit)
                                       .Include(x => x.Shirt)
                                       .FirstOrDefaultAsync(x => x.Id == id);
    }
}
