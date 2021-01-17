using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Dto.Abstractions;

namespace WeddingPlanner.Infrastructure.Dto
{
    public class ManOutfitDto : BaseOutfitDto
    {
        public CostDescription Suit { get; set; }
        public CostDescription Shirt { get; set; }
    }
}
