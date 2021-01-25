using System.Collections.Generic;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Dto.Abstractions;

namespace WeddingPlanner.Infrastructure.Dto
{
    public class OutfitDto : IDto
    {
        public int Id { get; set; }
        public IEnumerable<CostDescription> Costs { get; set; }
    }
}
