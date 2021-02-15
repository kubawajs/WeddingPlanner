using WeddingPlanner.Api.Models.Abstractions;
using WeddingPlanner.Infrastructure.Dto;

namespace WeddingPlanner.Api.Models
{
    public class GuestListResponse : BaseApiResponse<GuestListDto>
    {
        public int Count { get; set; }
        public int AgeParam { get; set; }

        public GuestListResponse()
        { }

        public GuestListResponse(BaseApiResponse<GuestListDto> response)
        {
            Message = response.Message;
            Status = response.Status;
            Result = response.Result;
            Item = response.Item;
        }
    }
}
