using Presentation.Responses;
using Shared.Exceptions;
using System.Diagnostics;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex).ConfigureAwait(false);
                return;

            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var traceId = Activity.Current?.Id ?? context.TraceIdentifier;
            int statusCode = StatusCodes.Status500InternalServerError;
            string title = "An unexpected error occurred.";
            object? errors = null;

            // Remove the switch statement to avoid CS0136 and CS0163
            if (exception is EntityNotFoundException e)
            {
                statusCode = e.StatusCode;
                title = e.Message;
                errors = e.Errors;
            }
            else if (exception is ApiException apiEx)
            {
                statusCode = apiEx.StatusCode;
                title = apiEx.Message;
                errors = apiEx.Errors;
            }
            else if (exception is UnauthorizedAccessException)
            {
                statusCode = StatusCodes.Status401Unauthorized;
                title = exception.Message;
                
            }
            else
            {
                //Log exception with trace id and request path
                _logger.LogError(exception, "Unhandled exception (TraceId: {TraceId}) at {Path}", traceId, context.Request?.Path.Value);
                
            }


            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var payload = ApiResponse<object>.Fail(message: title, errors: new
            {
                traceId,
                details = _env.IsDevelopment() ? exception.ToString() : errors,
                extra = errors,
            });

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };

            var json = JsonSerializer.Serialize(payload, options);
            await context.Response.WriteAsync(json).ConfigureAwait(false);
        }
    }
}
