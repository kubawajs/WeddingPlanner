using WeddingPlanner.Infrastructure.Models.Abstractions;

namespace WeddingPlanner.Infrastructure.Models.Authentication
{
    public class RegisterResponse : BaseApiResponse
    {
        public string Username { get; set; }

        public RegisterResponse()
        { }

        public RegisterResponse(BaseApiResponse response)
        {
            Message = response.Message;
            Status = response.Status;
            Result = response.Result;
        }
    }
}
