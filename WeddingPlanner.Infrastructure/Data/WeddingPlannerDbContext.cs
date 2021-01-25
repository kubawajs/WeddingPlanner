using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Infrastructure.Data
{
    public class WeddingPlannerDbContext : IdentityDbContext<User>
    {
        public DbSet<Guest> Guests { get; set; }
        public DbSet<WeddingHall> WeddingHalls { get; set; }
        public DbSet<Outfit> Outfits { get; internal set; }

        public WeddingPlannerDbContext(DbContextOptions<WeddingPlannerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Man outfit model
            //builder.Entity<ManOutfit>()
            //       .HasOne(x => x.Suit)
            //       .WithOne(x => x.ManOutfitItem)
            //       .HasForeignKey<CostDescription>(x => x.ManOutfitItemId);
        }
    }
}
