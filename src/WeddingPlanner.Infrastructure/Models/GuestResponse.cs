using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models.Abstractions;

namespace WeddingPlanner.Infrastructure.Models
{
    public class GuestResponse : BaseApiResponse<GuestDto>
    {
        public GuestResponse()
        { }

        public GuestResponse(BaseApiResponse<GuestDto> response)
        {
            Message = response.Message;
            Status = response.Status;
            Result = response.Result;
            Item = response.Item;
        }
    }
}