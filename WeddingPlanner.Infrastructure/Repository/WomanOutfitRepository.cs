using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Data;
using WeddingPlanner.Infrastructure.Repository.Abstractions;

namespace WeddingPlanner.Infrastructure.Repository
{
    public class WomanOutfitRepository : BaseOutfitRepository<WomanOutfit>
    {
        public WomanOutfitRepository(WeddingPlannerDbContext context)
            : base(context)
        { }

        public override async Task CreateOutfitAsync(WomanOutfit outfit)
        {
            Context.WomanOutfits.Add(outfit);
            await Context.SaveChangesAsync();
        }

        public override async Task<WomanOutfit> GetOutfitAsync(int id) 
            => await Context.WomanOutfits.Include(x => x.AdditionalCosts)
                                         .Include(x => x.Dress)
                                         .Include(x => x.Shoes)
                                         .FirstOrDefaultAsync(x => x.Id == id);
    }
}
