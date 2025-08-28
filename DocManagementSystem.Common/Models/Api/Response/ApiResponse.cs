using System.Net;

namespace DocManagementSystem.Common.Models.Api.Response
{
    public class ApiResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public string apiMessage { get; set; }
        public bool success { get; set; }
        public object? data { get; set; }
        public static ApiResponse Okay(object data = null, string messsage = null)
        {
            return new ApiResponse()
            {
                statusCode = HttpStatusCode.OK,
                apiMessage = messsage == null ? "Request successfully completed!" : messsage,
                data = data,
                success = true
            };
        }
        public static ApiResponse BadRequest(string message)
        {
            return new ApiResponse()
            {
                statusCode = HttpStatusCode.BadRequest,
                apiMessage = message,
                success = false
            };
        }

        public static ApiResponse NotFound(string str)
        {
            return new ApiResponse()
            {
                statusCode = HttpStatusCode.NotFound,
                apiMessage = str,
                success = false
            };
        }

        public static ApiResponse Forbidden(string str)
        {
            return new ApiResponse()
            {
                statusCode = HttpStatusCode.Forbidden,
                apiMessage = str,
                success = false
            };
        }

        public static ApiResponse Conflict(string str)
        {
            return new ApiResponse()
            {
                statusCode = HttpStatusCode.Conflict,
                apiMessage = str,
                success = false
            };
        }
        public static ApiResponse Unauthorized(string str)
        {
            return new ApiResponse()
            {
                statusCode = HttpStatusCode.Unauthorized,
                apiMessage = str,
                success = false
            };
        }

        public static ApiResponse InternalServerError(string str)
        {
            return new ApiResponse()
            {
                statusCode = HttpStatusCode.InternalServerError,
                apiMessage = str,
                success = false
            };
        }
        public static ApiResponse ExpectationFailed(string message)
        {
            return new ApiResponse()
            {
                statusCode = HttpStatusCode.ExpectationFailed,
                apiMessage = message,
                success = false
            };
        }

    }
}
