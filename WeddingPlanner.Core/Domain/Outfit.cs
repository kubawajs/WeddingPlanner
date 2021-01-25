using System.Collections.Generic;
using WeddingPlanner.Core.Domain.Abstractions;

namespace WeddingPlanner.Core.Domain
{
    public class Outfit : BaseModel
    {
        public IEnumerable<CostDescription> Costs { get; set; }
    }
}
