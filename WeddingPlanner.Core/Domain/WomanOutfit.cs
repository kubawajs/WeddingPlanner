using WeddingPlanner.Core.Domain.Abstractions;

namespace WeddingPlanner.Core.Domain
{
    public class WomanOutfit : BaseOutfit
    {
        public CostDescription Dress { get; set; }
        public CostDescription Hair { get; set; }
        public CostDescription MakeUp { get; set; }
    }
}
