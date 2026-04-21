using CineTrack.App.Common.Exceptions;

namespace CineTrack.Api.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            var statusCode = ex switch
            {
                AppValidationException => StatusCodes.Status400BadRequest,
                AppNotFoundException => StatusCodes.Status404NotFound,
                AppForbiddenException => StatusCodes.Status403Forbidden,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };
            
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";
            
            await httpContext.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
    }
}

public static class ErrorHandlingMiddlewareExtensions
{
    public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}