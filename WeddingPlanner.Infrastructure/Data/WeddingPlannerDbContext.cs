using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Core.Domain.Abstractions;

namespace WeddingPlanner.Infrastructure.Data
{
    public class WeddingPlannerDbContext : IdentityDbContext<User>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbSet<Guest> Guests { get; set; }
        public DbSet<WeddingHall> WeddingHalls { get; set; }
        public DbSet<Outfit> Outfits { get; internal set; }
        public DbSet<CostDescription> CostDescriptions { get; set; }

        public WeddingPlannerDbContext(
            DbContextOptions<WeddingPlannerDbContext> options,
            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var user = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUsername = user ?? "Anonymous";
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                if (entry.Entity is not BaseModel trackable)
                {
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Modified:
                        trackable.UpdatedOn = utcNow;
                        trackable.UpdatedByUsername = currentUsername;
                        entry.Property("CreatedOn").IsModified = false;
                        entry.Property("CreatedBy").IsModified = false;
                        break;

                    case EntityState.Added:
                        trackable.CreatedOn = utcNow;
                        trackable.UpdatedOn = utcNow;
                        trackable.CreatedByUsername = currentUsername;
                        trackable.UpdatedByUsername = currentUsername;
                        break;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Guests
            builder.Entity<Guest>()
                   .HasOne(x => x.CreatedBy)
                   .WithMany()
                   .HasForeignKey(x => x.CreatedByUsername)
                   .HasPrincipalKey(x => x.UserName);

            builder.Entity<Guest>()
                   .HasOne(x => x.UpdatedBy)
                   .WithMany()
                   .HasForeignKey(x => x.UpdatedByUsername)
                   .HasPrincipalKey(x => x.UserName);

            // Outfit
            builder.Entity<Outfit>()
                   .HasOne(x => x.CreatedBy)
                   .WithMany()
                   .HasForeignKey(x => x.CreatedByUsername)
                   .HasPrincipalKey(x => x.UserName);

            builder.Entity<Outfit>()
                   .HasOne(x => x.UpdatedBy)
                   .WithMany()
                   .HasForeignKey(x => x.UpdatedByUsername)
                   .HasPrincipalKey(x => x.UserName);

            // Wedding Halls
            builder.Entity<WeddingHall>()
                   .HasOne(x => x.CreatedBy)
                   .WithMany()
                   .HasForeignKey(x => x.CreatedByUsername)
                   .HasPrincipalKey(x => x.UserName);

            builder.Entity<WeddingHall>()
                   .HasOne(x => x.UpdatedBy)
                   .WithMany()
                   .HasForeignKey(x => x.UpdatedByUsername)
                   .HasPrincipalKey(x => x.UserName);
        }
    }
}
