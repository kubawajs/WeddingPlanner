using System;

namespace WeddingPlanner.Infrastructure.Dto.Abstractions
{
    public class BaseDto : IDto
    {
        public int Id { get; set; }

        public UserDto CreatedBy { get; set; }
        public UserDto UpdatedBy { get; set; }

        public DateTime CreatedOn { get; }
        public DateTime UpdatedOn { get; set; }
    }
}
