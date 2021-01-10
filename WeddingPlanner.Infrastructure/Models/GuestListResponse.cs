using System.Collections.Generic;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models.Abstractions;

namespace WeddingPlanner.Infrastructure.Models
{
    public class GuestListResponse : BaseApiResponse
    {
        public int Count { get; set; }
        public IEnumerable<GuestDto> Items { get; set; }
        public int AgeParam { get; set; }

        public GuestListResponse()
        { }

        public GuestListResponse(BaseApiResponse response)
        {
            Message = response.Message;
            Status = response.Status;
            Result = response.Result;
        }
    }
}
