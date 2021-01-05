using System;
using WeddingPlanner.Infrastructure.Dto.Abstractions;

namespace WeddingPlanner.Infrastructure.Dto
{
    public class GuestDto : IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsChild { get; set; }
        public bool HasPair { get; set; }
        public DateTime BirthDate { get; set; }

        public int Age() => DateTime.Now.Year - BirthDate.Year;
        public string FullName => $"{FirstName} {LastName}";
    }
}
