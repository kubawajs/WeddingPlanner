namespace WeddingPlanner.Infrastructure.Models
{
    public class CostDescription
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }

        // TODO: per person/summary ??
    }
}