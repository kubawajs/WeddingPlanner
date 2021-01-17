using WeddingPlanner.Core.Domain.Abstractions;

namespace WeddingPlanner.Core.Domain
{
    public class ManOutfit : BaseOutfit
    {
        public CostDescription Suit { get; set; }
        public CostDescription Shirt { get; set; }
    }
}
