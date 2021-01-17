using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Dto.Abstractions;

namespace WeddingPlanner.Infrastructure.Dto
{
    public class WomanOutfitDto : BaseOutfitDto
    {
        public CostDescription Dress { get; set; }
    }
}
