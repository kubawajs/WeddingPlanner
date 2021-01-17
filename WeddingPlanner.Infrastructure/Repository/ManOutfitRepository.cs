using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Data;
using WeddingPlanner.Infrastructure.Repository.Abstractions;

namespace WeddingPlanner.Infrastructure.Repository
{
    public class ManOutfitRepository : BaseOutfitRepository<ManOutfit>
    {
        public ManOutfitRepository(WeddingPlannerDbContext context)
            : base(context)
        { }

        public override async Task CreateOutfitAsync(ManOutfit outfit)
        {
            Context.ManOutfits.Add(outfit);
            await Context.SaveChangesAsync();
        }

        public override async Task<ManOutfit> GetOutfitAsync(int id) 
            => await Context.ManOutfits.Include(x => x.AdditionalCosts)
                                       .Include(x => x.Shoes)
                                       .Include(x => x.Suit)
                                       .Include(x => x.Shirt)
                                       .FirstOrDefaultAsync(x => x.Id == id);
    }
}
