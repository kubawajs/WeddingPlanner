using WeddingPlanner.Infrastructure.Dto.Abstractions;

namespace WeddingPlanner.Infrastructure.Dto
{
    public class UserDto : IDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
