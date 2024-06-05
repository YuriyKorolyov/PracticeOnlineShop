namespace MyApp.Middleware
{
    public class TaskCancellationHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TaskCancellationHandlingMiddleware> _logger;

        public TaskCancellationHandlingMiddleware(RequestDelegate next, ILogger<TaskCancellationHandlingMiddleware> logger)
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
            catch (TaskCanceledException)
            {
                _logger.LogWarning("Request was cancelled.");

                context.Response.StatusCode = 499; // Код 499 для отмененных запросов
                await context.Response.WriteAsync("Request was cancelled.");
            }
        }
    }
}
