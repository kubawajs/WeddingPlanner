using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models.Abstractions;

namespace WeddingPlanner.Infrastructure.Models
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
