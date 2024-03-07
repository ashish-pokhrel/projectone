using System;
namespace oneapp
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CustomExceptionHandler> logger;

        public CustomExceptionHandler(RequestDelegate next, ILogger<CustomExceptionHandler> logger)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Something went wrong!");

                // Customize the response for the exception
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                // Customize the error message based on your requirements
                var errorMessage = new { error = "Something went wrong!" };

                errorMessage = new { error = ex.Message.ToString() };
                await context.Response.WriteAsJsonAsync(errorMessage);
            }
        }
    }
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandler>();
        }
    }
}

