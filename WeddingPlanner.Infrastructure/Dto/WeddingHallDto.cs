using System.Collections.Generic;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Infrastructure.Dto
{
    public class WeddingHallDto
    {
        public int Id { get; set; }
        public int ChildAgeFrom { get; set; }
        public int ChildAgeTo { get; set; }
        public CostDescription MenuPerPerson { get; set; }
        public IEnumerable<CostDescription> AdditionalCosts { get; set; }
    }
}
