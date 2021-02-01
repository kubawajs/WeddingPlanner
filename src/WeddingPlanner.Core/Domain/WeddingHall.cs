using System.Collections.Generic;
using WeddingPlanner.Core.Domain.Abstractions;

namespace WeddingPlanner.Core.Domain
{
    public class WeddingHall : BaseModel
    {
        public int ChildAgeFrom { get; set; }
        public int ChildAgeTo { get; set; }
        public IEnumerable<CostDescription> Costs { get; set; }
    }
}
