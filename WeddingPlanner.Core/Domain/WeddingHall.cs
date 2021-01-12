using System.Collections.Generic;

namespace WeddingPlanner.Core.Domain
{
    public class WeddingHall
    {
        public int Id { get; internal set; }
        public int ChildAgeFrom { get; set; }
        public int ChildAgeTo { get; set; }
        public CostDescription MenuPerPerson { get; set; }
        public IEnumerable<CostDescription> AdditionalCosts { get; set; }
    }
}
