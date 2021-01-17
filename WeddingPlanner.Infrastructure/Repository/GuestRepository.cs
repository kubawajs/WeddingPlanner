using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Data;
using WeddingPlanner.Infrastructure.Repository.Abstractions;

namespace WeddingPlanner.Infrastructure.Repository
{
    public class GuestRepository : BaseRepository<Guest>, IGuestRepository
    {
        public GuestRepository(WeddingPlannerDbContext context)
            : base(context)
        { }

        public override async Task CreateAsync(Guest guest)
        {
            Context.Guests.Add(guest);
            await Context.SaveChangesAsync();
        }

        public override async Task<Guest> GetAsync(int id)
        {
            return await Context.Guests.FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IEnumerable<Guest>> GetAllAsync()
        {
            return await Context.Guests.ToListAsync();
        }

        public async Task<IEnumerable<Guest>> GetGuestsByAgeAsync(int age)
        {
            return await Context.Guests.Where(x => x.Age >= age || !x.IsChild).ToListAsync();
        }

        public async Task<int> GetAllGuestsCountAsync(int age = 0)
        {
            var guestsCount = await Context.Guests.Where(x => x.Age > age || !x.IsChild).CountAsync();
            var partnersCount = await Context.Guests.Where(x => x.HasPair == true && !x.IsChild).CountAsync();

            return await Task.FromResult(partnersCount + guestsCount);
        }

        public async Task<int> GetChildGuestsCountAsync(int childAgeFrom, int childAgeTo)
        {
            return await Context.Guests.Where(x => x.Age >= childAgeFrom && x.Age <= childAgeTo).CountAsync();
        }
    }
}
