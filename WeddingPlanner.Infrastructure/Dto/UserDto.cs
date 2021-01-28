using WeddingPlanner.Infrastructure.Dto.Abstractions;

namespace WeddingPlanner.Infrastructure.Dto
{
    public class UserDto : BaseDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
