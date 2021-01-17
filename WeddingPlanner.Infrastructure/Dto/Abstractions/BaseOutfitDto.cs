using System.Collections.Generic;
using WeddingPlanner.Core.Domain;

namespace WeddingPlanner.Infrastructure.Dto.Abstractions
{
    public class BaseOutfitDto : IDto
    {
        public int Id { get; set; }
        public CostDescription Shoes { get; set; }
        public CostDescription Accessories { get; set; }
        public IEnumerable<CostDescription> AdditionalCosts { get; set; }
    }
}
