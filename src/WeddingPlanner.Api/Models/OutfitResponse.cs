using WeddingPlanner.Api.Models.Abstractions;
using WeddingPlanner.Infrastructure.Dto;

namespace WeddingPlanner.Api.Models
{
    public class OutfitResponse<TModelDto> : BaseApiResponse<TModelDto>
        where TModelDto : OutfitDto
    {
        public OutfitResponse()
        { }

        public OutfitResponse(BaseApiResponse<TModelDto> response)
        {
            Message = response.Message;
            Status = response.Status;
            Result = response.Result;
            Item = response.Item;
        }
    }
}
