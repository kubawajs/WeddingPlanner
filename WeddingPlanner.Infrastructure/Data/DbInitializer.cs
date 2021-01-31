using System.Collections.Generic;
using System.Linq;
using WeddingPlanner.Core.Domain;

namespace WeddingPlanner.Infrastructure.Data
{
    public class DbInitializer
    {
        public static void Initialize(WeddingPlannerDbContext context)
        {
            context.Database.EnsureCreated();

            if(!context.CostDescriptions.Any())
            {
                var costs = new CostDescription[]
                {
                    new CostDescription { Label = "Garnitur", Description = "Garnitur ślubny Pana Młodego.", Price = 2500, Type = Core.Domain.Abstractions.CostType.Man },
                    new CostDescription { Label = "Buty", Description = "Buty ślubne Pana Młodego.", Price = 400, Type = Core.Domain.Abstractions.CostType.Man },
                    new CostDescription { Label = "Koszula biała", Description = "Biała koszula ślubna Pana Młodego.", Price = 400, Type = Core.Domain.Abstractions.CostType.Man },
                    new CostDescription { Label = "Suknia", Description = "Suknia ślubna Panny Młodej.", Price = 5000, Type = Core.Domain.Abstractions.CostType.Woman },
                    new CostDescription { Label = "Buty", Description = "Buty ślubne Panny Młodej.", Price = 2500, Type = Core.Domain.Abstractions.CostType.Woman }
                };

                foreach(var cost in costs)
                {
                    context.CostDescriptions.Add(cost);
                }
            }

            if (!context.Guests.Any())
            {
                var guests = new Guest[]
                {
                    new Guest { FirstName = "Jan", LastName = "Kowalski", HasPair = false, IsChild = false },
                    new Guest { FirstName = "Adam", LastName = "Nowak", HasPair = true, IsChild = false },
                    new Guest { FirstName = "Krzysztof", LastName = "Nowak", HasPair = false, IsChild = false },
                    new Guest { FirstName = "Mateusz", LastName = "Nowak", HasPair = false, IsChild = false },
                    new Guest { FirstName = "Robert", LastName = "Lewandowski", HasPair = false, IsChild = false },
                    new Guest { FirstName = "Gracjan", LastName = "Roztocki", HasPair = false, IsChild = true, Age = 3 },
                    new Guest { FirstName = "Jerzy", LastName = "Król", HasPair = false, IsChild = false },
                    new Guest { FirstName = "Barbara", LastName = "Król", HasPair = false, IsChild = false }
                };

                foreach(var guest in guests)
                {
                    context.Guests.Add(guest);
                }
            }

            if(!context.Outfits.Any())
            {
                context.Outfits.Add(new Outfit());
            }

            if(!context.WeddingHalls.Any())
            {
                context.WeddingHalls.Add(new WeddingHall
                {
                    ChildAgeFrom = 5,
                    ChildAgeTo = 10,
                    Costs = new List<CostDescription>(),
                });
            }

            context.SaveChanges();
        }
    }
}
