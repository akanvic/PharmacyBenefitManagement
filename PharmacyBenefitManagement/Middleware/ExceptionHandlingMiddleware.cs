using PharmacyBenefitManagement.Exceptions;
using System.Net;
using System.Text.Json;

namespace PharmacyBenefitManagement.Middleware
{

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/problem+json";

            var problemDetails = exception switch
            {
                MemberNotFoundException notFoundEx => CreateProblemDetails(
                    context,
                    HttpStatusCode.NotFound,
                    "Member Not Found",
                    notFoundEx.Message,
                    "https://api.pbm.com/errors/not-found"
                ),

                ValidationException validationEx => CreateProblemDetails(
                    context,
                    HttpStatusCode.BadRequest,
                    "Validation Error",
                    validationEx.Message,
                    "https://api.pbm.com/errors/validation",
                    validationEx.Errors
                ),

                ExternalServiceException serviceEx => CreateProblemDetails(
                    context,
                    HttpStatusCode.ServiceUnavailable,
                    "External Service Error",
                    $"The {serviceEx.ServiceName} service is currently unavailable.",
                    "https://api.pbm.com/errors/service-unavailable"
                ),

                _ => CreateProblemDetails(
                    context,
                    HttpStatusCode.InternalServerError,
                    "Internal Server Error",
                    _environment.IsDevelopment()
                        ? exception.Message
                        : "An unexpected error occurred. Please try again later.",
                    "https://api.pbm.com/errors/internal-error"
                )
            };

            // Log with appropriate severity
            var logLevel = exception switch
            {
                MemberNotFoundException => LogLevel.Warning,
                ValidationException => LogLevel.Warning,
                _ => LogLevel.Error
            };

            _logger.Log(
                logLevel,
                exception,
                "Error processing request {Method} {Path}: {Message}",
                context.Request.Method,
                context.Request.Path,
                exception.Message
            );

            context.Response.StatusCode = problemDetails.Status ?? 500;
            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            }));
        }


        private static ProblemDetailsResponse CreateProblemDetails(
            HttpContext context,
            HttpStatusCode statusCode,
            string title,
            string detail,
            string type,
            Dictionary<string, string[]>? errors = null)
        {
            return new ProblemDetailsResponse
            {
                Type = type,
                Title = title,
                Status = (int)statusCode,
                Detail = detail,
                TraceId = context.TraceIdentifier,
                Instance = context.Request.Path,
                Errors = errors
            };
        }
    }

    public class ProblemDetailsResponse
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int? Status { get; set; }
        public string Detail { get; set; } = string.Empty;
        public string? TraceId { get; set; }
        public string? Instance { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }
    }
}
