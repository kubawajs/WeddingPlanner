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

        public WeddingPlannerDbContext(DbContextOptions<WeddingPlannerDbContext> options)
            : base(options)
        {
        }
    }
}
