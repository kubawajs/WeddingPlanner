using System.Collections.Generic;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Dto.Abstractions;

namespace WeddingPlanner.Infrastructure.Dto
{
    public class WeddingHallDto : IDto
    {
        public int Id { get; set; }
        public int ChildAgeFrom { get; set; }
        public int ChildAgeTo { get; set; }
        public CostDescription MenuPerPerson { get; set; }
        public IEnumerable<CostDescription> Costs { get; set; }
    }
}
