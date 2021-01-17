using WeddingPlanner.Infrastructure.Dto.Abstractions;
using WeddingPlanner.Infrastructure.Models.Abstractions;

namespace WeddingPlanner.Infrastructure.Models
{
    public class OutfitResponse<TModelDto> : BaseApiResponse<TModelDto>
        where TModelDto : BaseOutfitDto
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
