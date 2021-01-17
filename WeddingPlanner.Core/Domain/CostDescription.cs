using WeddingPlanner.Core.Domain.Abstractions;

namespace WeddingPlanner.Core.Domain
{
    public class CostDescription : BaseModel
    {
        public decimal Price { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }

        // TODO: per person/summary ??
    }
}