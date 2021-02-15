using WeddingPlanner.Api.Models.Abstractions;
using WeddingPlanner.Infrastructure.Dto;

namespace WeddingPlanner.Api.Models.Authentication
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
