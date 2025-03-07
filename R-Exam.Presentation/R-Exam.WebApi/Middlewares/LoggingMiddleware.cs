using R_Exam.Application.Services;

namespace R_Exam.Presentation.Middlewares
{
    public class LoggingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate next = next;

        public async Task InvokeAsync(HttpContext httpContext, ILogService logService)
        {
            logService.CreateRequestLog(httpContext);
            await next.Invoke(httpContext);
            logService.CreateResponseLog(httpContext);
        }
    }
}
