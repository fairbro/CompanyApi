using CompanyApi.Errors;
using CompanyApi.Exceptions;

namespace CompanyApi.Middleware
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

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(CompanyNotFoundException ex)
            {
                _logger.LogError(ex, "Company with id {id} not found", ex.Id);
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = "Not Found",
                    ErrorDescription = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception");

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = "Not Found",
                    ErrorDescription = "Company not found"
                });
            }
        }
    }
}
