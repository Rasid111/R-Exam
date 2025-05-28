using R_Exam.Application.Services;

namespace R_Exam.Presentation.Middlewares
{
    public class LoggerMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate next = next;

        public async Task InvokeAsync(HttpContext httpContext, ILogService logService)
        {
            _ = logService.CreateRequestLog(httpContext);
            await next.Invoke(httpContext);
            _ = logService.CreateResponseLog(httpContext);
        }
    }
}
