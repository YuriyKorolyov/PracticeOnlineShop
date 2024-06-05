namespace MyApp.Middleware
{
    public class TaskCancellationHandlingMiddleware
    {
        /// <summary>
        /// Промежуточное ПО для обработки отмены задач.
        /// </summary>
        private readonly RequestDelegate _next;
        private readonly ILogger<TaskCancellationHandlingMiddleware> _logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TaskCancellationHandlingMiddleware"/>.
        /// </summary>
        /// <param name="next">Следующий обработчик запроса.</param>
        /// <param name="logger">Логгер.</param>
        public TaskCancellationHandlingMiddleware(RequestDelegate next, ILogger<TaskCancellationHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Вызывает промежуточное ПО для обработки отмены задач.
        /// </summary>
        /// <param name="context">Контекст HTTP-запроса.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (TaskCanceledException)
            {
                _logger.LogWarning("Request was cancelled.");

                context.Response.StatusCode = 499; // Код 499 для отмененных запросов
                await context.Response.WriteAsync("Request was cancelled.");
            }
        }
    }
}
