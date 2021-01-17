using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models.Abstractions;

namespace WeddingPlanner.Infrastructure.Models.Authentication
{
    public class RegisterResponse : BaseApiResponse<UserDto>
    {
        public RegisterResponse()
        { }

        public RegisterResponse(BaseApiResponse<UserDto> response)
        {
            Message = response.Message;
            Status = response.Status;
            Result = response.Result;
            Item = response.Item;
        }
    }
}
