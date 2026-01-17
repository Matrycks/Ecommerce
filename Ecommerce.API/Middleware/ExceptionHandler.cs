
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Middleware
{
    /// <summary>
    /// Middleware for handling all request exceptions.
    /// </summary>
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler> _logger;
        private readonly IHostEnvironment _env;

        /// <summary>
        /// Middleware for handling all request exceptions.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        /// <param name="env"></param>
        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            string correlationId = context.TraceIdentifier;
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId
            }))
            {
                try
                {
                    var path = context.Request.Path.Value ?? string.Empty;

                    _logger.LogInformation("{path} called..", path);

                    await _next(context);

                    _logger.LogInformation("{path} succeeded..", path);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled exception");

                    await HandleExceptionAsync(context, ex);
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Title = context.Response.StatusCode switch
                {
                    StatusCodes.Status400BadRequest => "Bad Request",
                    StatusCodes.Status401Unauthorized => "Unauthorized",
                    404 => "Not Found",
                    _ => "Internal Server Error"
                },
                Detail = _env.IsDevelopment() ? ex.Message : "Oops.. something bad happened."
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}