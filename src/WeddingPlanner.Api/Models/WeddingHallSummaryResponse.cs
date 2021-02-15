using WeddingPlanner.Api.Models.Abstractions;
using WeddingPlanner.Infrastructure.Dto;

namespace WeddingPlanner.Api.Models
{
    public class WeddingHallSummaryResponse : BaseApiResponse<WeddingHallDto>
    {
        public int AdultGuestsCount { get; set; }
        public int ChildGuestsCount { get; set; }

        public WeddingHallSummaryResponse()
        { }

        public WeddingHallSummaryResponse(BaseApiResponse<WeddingHallDto> response)
        {
            Message = response.Message;
            Status = response.Status;
            Result = response.Result;
            Item = response.Item;
        }
    }
}
