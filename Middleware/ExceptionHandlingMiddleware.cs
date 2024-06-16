using System.Text.Json;

namespace MyApp.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        /// <summary>
        /// Промежуточное ПО для обработки отмены задач.
        /// </summary>
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ExceptionHandlingMiddleware"/>.
        /// </summary>
        /// <param name="logger">Логгер.</param>
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Вызывает промежуточное ПО для обработки отмены задач.
        /// </summary>
        /// <param name="context">Контекст HTTP-запроса.</param>
        /// <param name="next">Следующий обработчик запроса.</param>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (TaskCanceledException)
            {
                _logger.LogWarning("Request was cancelled.");
                await HandleTaskCanceledExceptionAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleTaskCanceledExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 499; 

            var result = JsonSerializer.Serialize(new { error = "Request was cancelled." });
            return context.Response.WriteAsync(result);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var result = JsonSerializer.Serialize(new { error = exception.Message });
            return context.Response.WriteAsync(result);
        }
    }
}
