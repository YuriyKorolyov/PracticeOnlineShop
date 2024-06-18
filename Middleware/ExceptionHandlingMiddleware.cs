using System.Text.Json;

namespace MyApp.Middleware
{
    /// <summary>
    /// Middleware для обработки исключений в HTTP-запросах.
    /// </summary>
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ExceptionHandlingMiddleware"/>.
        /// </summary>
        /// <param name="logger">Логгер для записи сообщений об ошибках.</param>
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Обрабатывает HTTP-запрос и вызывает следующее промежуточное ПО в цепочке запроса.
        /// </summary>
        /// <param name="context">Контекст HTTP-запроса.</param>
        /// <param name="next">Следующее промежуточное ПО в цепочке запроса.</param>
        /// <returns>Асинхронную задачу для обработки запроса.</returns>
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
            catch (UnauthorizedAccessException)
            {
                await HandleUnauthorizedAccessExceptionAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Обрабатывает исключение TaskCanceledException и возвращает соответствующий HTTP-ответ.
        /// </summary>
        /// <param name="context">Контекст HTTP-запроса.</param>
        /// <returns>Асинхронную задачу для обработки исключения.</returns>
        private Task HandleTaskCanceledExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 499; 

            var result = JsonSerializer.Serialize(new { error = "Request was cancelled." });
            return context.Response.WriteAsync(result);
        }

        /// <summary>
        /// Обрабатывает исключение UnauthorizedAccessException и возвращает соответствующий HTTP-ответ.
        /// </summary>
        /// <param name="context">Контекст HTTP-запроса.</param>
        /// <returns>Асинхронную задачу для обработки исключения.</returns>
        private Task HandleUnauthorizedAccessExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            var result = JsonSerializer.Serialize(new { error = "Unauthorized access." });
            return context.Response.WriteAsync(result);
        }

        /// <summary>
        /// Обрабатывает общее исключение и возвращает соответствующий HTTP-ответ с информацией об ошибке.
        /// </summary>
        /// <param name="context">Контекст HTTP-запроса.</param>
        /// <param name="exception">Исключение, которое произошло во время обработки запроса.</param>
        /// <returns>Асинхронную задачу для обработки исключения.</returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var result = JsonSerializer.Serialize(new { error = exception.Message });
            return context.Response.WriteAsync(result);
        }
    }
}
