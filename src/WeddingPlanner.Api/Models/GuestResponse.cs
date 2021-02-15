using WeddingPlanner.Api.Models.Abstractions;
using WeddingPlanner.Infrastructure.Dto;

namespace WeddingPlanner.Api.Models
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