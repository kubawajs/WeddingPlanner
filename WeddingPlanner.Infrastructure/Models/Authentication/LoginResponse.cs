using System;
using WeddingPlanner.Infrastructure.Models.Abstractions;

namespace WeddingPlanner.Infrastructure.Models.Authentication
{
    public class LoginResponse : BaseApiResponse
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public LoginResponse(BaseApiResponse response)
        {
            Message = response.Message;
            Status = response.Status;
            Result = response.Result;
        }
    }
}
