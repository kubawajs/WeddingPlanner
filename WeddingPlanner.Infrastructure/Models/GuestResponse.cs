using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models.Abstractions;

namespace WeddingPlanner.Infrastructure.Models
{
    public class GuestResponse : BaseApiResponse
    {
        public GuestDto Item { get; set; }

        public GuestResponse()
        { }

        public GuestResponse(BaseApiResponse response)
        {
            Message = response.Message;
            Status = response.Status;
            Result = response.Result;
        }
    }
}