namespace CryptoExchangeApi.Middleware;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Proceed to the next middleware
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred."); // Log the error
            await HandleExceptionAsync(context, ex); // Handle the exception
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            ArgumentException => StatusCodes.Status400BadRequest, // Example: Bad Request for argument exceptions
            KeyNotFoundException => StatusCodes.Status404NotFound, // Example: Not Found for missing resources
            _ => StatusCodes.Status500InternalServerError // Default: Internal Server Error
        };

        var errorResponse = new
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message, // You can customize the message
            Detailed = exception.StackTrace // Include stack trace in development only
        };

        return context.Response.WriteAsJsonAsync(errorResponse);
    }
}
