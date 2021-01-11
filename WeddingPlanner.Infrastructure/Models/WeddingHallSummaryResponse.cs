using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models.Abstractions;

namespace WeddingPlanner.Infrastructure.Models
{
    public class WeddingHallSummaryResponse : BaseApiResponse
    {
        public int AdultGuestsCount { get; set; }
        public int ChildGuestsCount { get; set; }
        public WeddingHallDto Summary { get; set; }

        public WeddingHallSummaryResponse()
        { }

        public WeddingHallSummaryResponse(BaseApiResponse response)
        {
            Message = response.Message;
            Status = response.Status;
            Result = response.Result;
        }
    }
}
