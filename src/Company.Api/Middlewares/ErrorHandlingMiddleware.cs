using Company.Api.Models;
using System.Net;

namespace Company.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the request.");

                var errorResponse = new ErrorResponse
                {
                    Message = "An unexpected error occurred.",
                    Detail = ex.Message,
                    StackTrace = ex.StackTrace,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };

                httpContext.Response.StatusCode = errorResponse.StatusCode;
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }

            if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                var unauthorizedResponse = new ErrorResponse
                {
                    Message = "Unauthorized. Please provide valid credentials.",
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsJsonAsync(unauthorizedResponse);
            }

            else if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                var notFoundResponse = new ErrorResponse
                {
                    Message = "Resource not found.",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsJsonAsync(notFoundResponse);
            }

            else if (httpContext.Response.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                var badRequestResponse = new ErrorResponse
                {
                    Message = "Bad Request. Check your input data.",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsJsonAsync(badRequestResponse);
            }
        }
    }
}
