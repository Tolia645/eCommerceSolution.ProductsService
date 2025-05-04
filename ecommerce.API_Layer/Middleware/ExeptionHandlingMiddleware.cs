namespace ecommerce.API_Layer.Middleware;

public class ExeptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExeptionHandlingMiddleware> _logger;
    public ExeptionHandlingMiddleware(RequestDelegate next, ILogger<ExeptionHandlingMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError($"{ex.GetType().ToString()}: {ex.Message}");

            if (ex.InnerException is not null)
            {
                _logger.LogError($"{ex.InnerException.GetType().ToString()}: {ex.InnerException.Message}");
            }
            
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { Message = ex.Message, Type = ex.GetType().ToString() });
        }
    }
}

public static class ExeptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExeptionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExeptionHandlingMiddleware>();
    }
}