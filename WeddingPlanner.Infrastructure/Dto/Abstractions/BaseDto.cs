using System;

namespace WeddingPlanner.Infrastructure.Dto.Abstractions
{
    public class BaseDto : IDto
    {
        public int Id { get; set; }

        public UserDto CreatedBy { get; }
        public UserDto UpdatedBy { get; set; }

        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; set; }
    }
}
