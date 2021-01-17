using System.Collections.Generic;

namespace WeddingPlanner.Core.Domain.Abstractions
{
    public class BaseOutfit : BaseModel
    {
        public CostDescription Shoes { get; set; }
        public CostDescription Accessories { get; set; }
        public IEnumerable<CostDescription> AdditionalCosts { get; set; }
    }
}
