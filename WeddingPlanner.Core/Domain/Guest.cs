using WeddingPlanner.Core.Domain.Abstractions;

namespace WeddingPlanner.Core.Domain
{
    public class Guest : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsChild { get; set; }
        public bool HasPair { get; set; }
        public int Age { get; set; }
    }
}
