using System;

namespace WeddingPlanner.Core.Domain
{
    public class Guest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsChild { get; set; }
        public bool HasPair { get; set; }
        public int Age { get; set; }
    }
}
