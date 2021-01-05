using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Infrastructure.Data
{
    public class WeddingPlannerDbContext : DbContext
    {
        public DbSet<Guest> Guests { get; set; }

        public WeddingPlannerDbContext(DbContextOptions<WeddingPlannerDbContext> options)
            : base(options)
        {
        }
    }
}
