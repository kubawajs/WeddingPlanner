using System;
using WeddingPlanner.Infrastructure.Dto.Abstractions;

namespace WeddingPlanner.Infrastructure.Dto
{
    public class GuestDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsChild { get; set; }
        public bool HasPair { get; set; }
        public int Age { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
