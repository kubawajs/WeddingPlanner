namespace WeddingPlanner.Infrastructure.Models
{
    public class BaseApiResponse
    {
        public bool Result { get; set; }
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }

        public static BaseApiResponse CreateErrorResponse(string message)
            => new BaseApiResponse
            {
                Result = false,
                Status = ResponseStatus.Error,
                Message = message
            };

        public static BaseApiResponse CreateSuccessResponse(string message)
            => new BaseApiResponse
            {
                Result = true,
                Status = ResponseStatus.Success,
                Message = message
            };
    }
}
