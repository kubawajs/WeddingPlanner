using WeddingPlanner.Infrastructure.Dto.Abstractions;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Api.Models.Abstractions
{
    public class BaseApiResponse<TDto>
        where TDto : IDto
    {
        public bool Result { get; set; }
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public TDto Item { get; set; }

        public static BaseApiResponse<TDto> CreateErrorResponse(string message)
            => new BaseApiResponse<TDto>
            {
                Result = false,
                Status = ResponseStatus.Error,
                Message = message
            };

        public static BaseApiResponse<TDto> CreateSuccessResponse(string message, TDto item)
            => new BaseApiResponse<TDto>
            {
                Result = true,
                Status = ResponseStatus.Success,
                Message = message,
                Item = item
            };
    }
}
