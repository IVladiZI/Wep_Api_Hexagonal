using Domain.Entities;

namespace Application
{
    public static class ApiResponseFactory
    {
        public static ApiResponse Success(object? data, string message = "OK", string? title = null)
        {
            return new ApiResponse
            {
                State = 200,
                Message = message,
                Data = data,
                Title = title,
                Code = "0"
            };
        }

        public static ApiResponse ControlledError(string message, object? exceptions = null, string? title = null)
        {
            return new ApiResponse
            {
                State = 400,
                Message = message,
                Data = null,
                Exceptions = exceptions,
                Title = title,
                Code = "1"
            };
        }

        public static ApiResponse ServerError(string message, object? exceptions = null, string? title = null)
        {
            return new ApiResponse
            {
                State = 500,
                Message = message,
                Data = null,
                Exceptions = exceptions,
                Title = title,
                Code = "SERVER_ERROR"
            };
        }
    }
}
